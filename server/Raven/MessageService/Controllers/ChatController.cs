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
public class ChatController(ChatService chatService) : ControllerBase
{
    private string UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    
    [HttpGet("list")]
    public async Task<IActionResult> GetChatList()
    {
        return Ok(await chatService.GetChatList(UserId));
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateChat([FromBody] CreateSimpleChatRequest request)
    { 
        if (request.UserOne != UserId) return Unauthorized();
        
        var chatId = await chatService.CreateSimpleChatAsync(request.UserOne, request.UserTwo);
        return Ok(chatId);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddUserToChat([FromBody] AddChatRequest request)
    {
        var chatId = request.ChatId;
        await chatService.AddUserToChatAsync(chatId, UserId);
        return Ok();
    }
}