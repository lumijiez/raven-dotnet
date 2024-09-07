using Microsoft.AspNetCore.Mvc;
using Raven.Auth.Application.Models;
using Raven.Auth.Application.Services;
using Raven.Auth.Domain.Services;

namespace Raven.Auth.Controllers;

[ApiController]
[Route("/")]
public class AuthController(AuthAppService authAppService, JwtTokenHelper tokenHelper) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserDTO dto)
    {
        try
        {
            authAppService.RegisterUser(dto);
            var token = tokenHelper.GenerateToken(dto.Username);
            var refreshToken = tokenHelper.GenerateRefreshToken();
            
            return Ok(new
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // To Fix (Not Secure)
    // Does not check validity, creates a token from any input
    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] RefreshTokenDTO dto)
    {
        var principal = tokenHelper.GetPrincipalFromExpiredToken(dto.Token);
        
        if (principal.Identity is not { Name: not null }) return BadRequest();
        
        var username = principal.Identity.Name;
    
        var newToken = tokenHelper.GenerateToken(username);
        var newRefreshToken = tokenHelper.GenerateRefreshToken();
            
        return Ok(new
        {
            Token = newToken,
            RefreshToken = newRefreshToken
        });

    }
}