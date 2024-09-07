using AspNet.Security.OAuth.Discord;

namespace Raven.Auth.Controllers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("oauth/discord")]
public class DiscordOAuthController : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        var redirectUrl = Url.Action("Response", "DiscordOAuth", null, Request.Scheme);
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, DiscordAuthenticationDefaults.AuthenticationScheme);
    }

    [HttpGet("response")]
    public async Task<IActionResult> Response()
    {
        var result = await HttpContext.AuthenticateAsync(DiscordAuthenticationDefaults.AuthenticationScheme);
        var claims = result.Principal?.Identities.FirstOrDefault()
            ?.Claims.Select(claim => new
        {
            claim.Issuer,
            claim.OriginalIssuer,
            claim.Type,
            claim.Value
        });
        
        Console.WriteLine(claims);
            
        if (result.Succeeded)
        {
            return Json(claims);
        }
            
        return RedirectToAction("Login");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
}
