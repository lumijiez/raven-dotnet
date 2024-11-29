using Microsoft.AspNetCore.Mvc;
using Raven.Auth.Interfaces;

namespace Raven.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeyController(IJwtService jwtService) : ControllerBase
{
    [HttpGet("public")]
    public IActionResult GetPublicKey()
    {
        return Ok(jwtService.GetPublicKey());
    }
}