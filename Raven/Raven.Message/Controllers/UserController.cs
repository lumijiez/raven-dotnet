using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Message.Application.Models;
using Raven.Message.Application.Services;
using Raven.Message.Domain.Entities;

namespace Raven.Message.Controllers;

[ApiController]
[Route("api/message/user/")]
[Authorize]
public class UserProfileController(UserService userService) : ControllerBase
{
    private string UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    
    [HttpPost("add")]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
    {
        var newUser = await userService.AddUserAsync(new UserProfile
        {
            Bio = request.Bio,
            DisplayName = request.DisplayName,
            UserId = UserId,
            Username = request.Username
        });
        
        return CreatedAtAction(nameof(GetUser), new { userId = newUser.UserId }, newUser);
    }
        
    [HttpGet("/info")]
    public async Task<IActionResult> GetUser(string userId)
    {
        var user = await userService.GetUserByIdAsync(userId);
        return Ok(user);
    }
}