using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Raven.Message.Application.Handlers;
using Raven.Message.Application.Services;

namespace Raven.Message.SignalR;

[Authorize]
public class ChatHub(MessageHandler messageHandler, ChatHandler chatHandler, ChatService chatService) : Hub
{
    private static readonly ConcurrentDictionary<string, string?> UserConnectionMap = new();
    
    public override async Task OnConnectedAsync()
    {
        UserConnectionMap[Context.UserIdentifier!] = Context.ConnectionId;
        await SendFromSystem(Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public async Task SendFromSystem(string receiverId)
    {
        await Clients.Client(receiverId).SendAsync("ReceiveSystem", "Successfully Joined.");
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
    
    public async Task CreateChat(string otherUserId)
    {
        var currentUserId = Context.UserIdentifier;
        if (currentUserId == null || currentUserId == otherUserId)
        {
            await Clients.Caller.SendAsync("ReceiveSystem", "Cannot create chat.");
            return;
        }

        var newChatId = await chatService.CreateSimpleChatAsync(currentUserId, otherUserId);
        
        if (UserConnectionMap.TryGetValue(currentUserId, out var currentUserConnectionId))
        {
            await Clients.Client(currentUserConnectionId).SendAsync("ReceiveSystem", "Chat Created");
            await Clients.Client(currentUserConnectionId).SendAsync("ChatCreated", newChatId, otherUserId);
        }
        
        if (UserConnectionMap.TryGetValue(otherUserId, out var otherUserConnectionId))
        {
            await Clients.Client(currentUserConnectionId).SendAsync("ReceiveSystem", "Chat Created");
            await Clients.Client(otherUserConnectionId).SendAsync("ChatCreated", newChatId, currentUserId);
        }
    }
    
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        UserConnectionMap.TryRemove(Context.UserIdentifier, out _);
        await base.OnDisconnectedAsync(exception);
    }
}