using Microsoft.AspNetCore.Mvc;
using Raven.Auth.Application.Models;
using Raven.Auth.Application.Services;

namespace Raven.Auth.Controllers;

[ApiController]
[Route("/")]
public class AuthController(AuthAppService authAppService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserDTO dto)
    {
        try
        {
            authAppService.RegisterUser(dto);
            return Ok("User registered successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}