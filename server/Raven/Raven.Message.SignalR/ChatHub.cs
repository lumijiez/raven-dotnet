using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Raven.Message.Application.Handlers;

namespace Raven.Message.SignalR;

[Authorize]
public class ChatHub(MessageHandler messageHandler, ChatHandler chatHandler) : Hub
{
    private static readonly ConcurrentDictionary<string, string?> UserConnectionMap = new();
    
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier!;
        
        UserConnectionMap[userId] = Context.ConnectionId;
        
        await Clients.All.SendAsync("ReceiveMessage", "System",
            $"{Context.UserIdentifier} joined.");
        
        await SendFromSystem(Context.ConnectionId);
        
        Console.WriteLine($"{Context.UserIdentifier} joined.");
        await base.OnConnectedAsync();
    }

    public async Task SendFromSystem(string receiverId)
    {
        await Clients.Client(receiverId).SendAsync("ReceiveMessage", "System", "You have successfully joined the chat hub.");
    }
    
    public async Task SendMsg(string chatId, string message)
    {
        var participants = await chatHandler.GetChatDetailsAsync(chatId);
        
        Console.WriteLine($"{Context.UserIdentifier}: {chatId}: {message}");
        
        foreach (var participantId in participants.Participants)
        {
            if (UserConnectionMap.TryGetValue(participantId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", chatId, Context.UserIdentifier, message);
                await messageHandler.AddMessage(new Domain.Entities.Message
                {
                    Text = message,
                    Timestamp = DateTime.Now
                });
            }
            else
            {
                Console.WriteLine($"No user with ID {participantId} is connected.");
            }
        }
    }


    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier;
        UserConnectionMap.TryRemove(userId, out _);

        Console.WriteLine(userId + " left the chat hub.");
        await base.OnDisconnectedAsync(exception);
    }
}