using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Raven.Auth.Dtos;
using Raven.Auth.Interfaces;
using Raven.Auth.Models;

namespace Raven.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await authService.RegisterAsync(registerDto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await authService.LoginAsync(loginDto);
        return Ok(result);
    }
    
    [HttpGet("google-login")]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = Url.Action("GoogleCallback", "Auth");
        var properties = new AuthenticationProperties 
        { 
            RedirectUri = redirectUrl 
        };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
            return Unauthorized();

        var emailClaim = authenticateResult.Principal?.FindFirst(ClaimTypes.Email)?.Value;
        var nameClaim = authenticateResult.Principal?.FindFirst(ClaimTypes.Name)?.Value;
        var googleIdClaim = authenticateResult.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (emailClaim == null || googleIdClaim == null)
            return BadRequest("Google authentication failed: Missing required claims.");

        var user = await authService.FindUserByEmailAsync(emailClaim);

        if (user == null)
        {
            user = new AppUser
            {
                UserName = emailClaim,
                Email = emailClaim,
                GoogleId = googleIdClaim,
                IsExternalAccountLinked = true
            };
            await authService.CreateUserAsync(user);
        }
        else if (string.IsNullOrEmpty(user.GoogleId))
        {
            user.GoogleId = googleIdClaim;
            user.IsExternalAccountLinked = true;
            await authService.UpdateUserAsync(user);
        }

        var jwtToken = authService.GenerateJwtToken(user);

        return Ok(new { Token = jwtToken });
    }
}