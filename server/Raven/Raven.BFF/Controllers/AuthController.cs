using Microsoft.AspNetCore.Mvc;
using Raven.BFF.Application.Models.Requests;
using Raven.BFF.Application.Services;
namespace Raven.BFF.Controllers;

[ApiController]
[Route("/auth/")]
public class AuthController(AuthenticationService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] RavenLoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Invalid login request");
        }
        
        var result = await authService.LoginUserAsync(request);

        if (result.Success is true)  
        {
            return Ok(result.Data); 
        }
        
        return Unauthorized(new { Message = "Authentication failed" });
    }
}