using System.Collections.Concurrent;
using MessageService.Application.Handlers;
using MessageService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MessageService.SignalR;

[Authorize]
public class ChatHub(MessageHandler messageHandler) : Hub
{
    private MessageHandler _messageHandler = messageHandler;

    private static readonly ConcurrentDictionary<string, string> UserConnectionMap = new();
    
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
    
    public async Task SendMsg(string receiverId, string message)
    {
        if (UserConnectionMap.TryGetValue(receiverId, out string connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", Context.UserIdentifier, message);
            await messageHandler.AddMessage(new Message
            {
                Text = message,
                Timestamp = DateTime.Now
            });
        }
        else
        {
            Console.WriteLine($"No user with ID {receiverId} is connected.");
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        string userId = Context.UserIdentifier;
        UserConnectionMap.TryRemove(userId, out _);

        Console.WriteLine(userId + " left the chat hub.");
        await base.OnDisconnectedAsync(exception);
    }
}