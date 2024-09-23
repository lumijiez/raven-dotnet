using MessageService.Application;
using MessageService.Application.Models;
using MessageService.Application.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.VisualBasic.CompilerServices;

namespace MessageService.Controllers;

[Route("chat/")]
[ApiController]
[Authorize]
public class ChatController(UserChatHandler userChatHandler, ChatHandler chatHandler) : ControllerBase
{
    private string GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetChatList()
    {
        var userId = GetUserId();
        return Ok(await userChatHandler.GetChatsByUserId(userId));
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddUserToChat([FromBody] AddChatRequest request)
    {
        var userId = GetUserId();
        var chatId = request.ChatId;
        await userChatHandler.AddChat(userId, chatId);
        await chatHandler.AddUser(IntegerType.FromString(userId), IntegerType.FromString(chatId));
        return Ok();
    }
}