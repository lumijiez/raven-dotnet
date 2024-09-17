using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MessageService;

public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveSystemMessage", 
            $"{Context.UserIdentifier} joined.");
        Console.WriteLine($"{Context.UserIdentifier} joined.");
        await base.OnConnectedAsync();
    }
}