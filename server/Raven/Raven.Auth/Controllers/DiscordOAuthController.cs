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
        var redirectUrl = Url.Action("RespCallback", "DiscordOAuth", null, Request.Scheme);
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, DiscordAuthenticationDefaults.AuthenticationScheme);
    }

    [HttpGet("respcallback")]
    public async Task<IActionResult> RespCallback()
    {
        var result = await HttpContext.AuthenticateAsync(DiscordAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded) return RedirectToAction("Login");
            
        var tokens = result.Properties.GetTokens();
                
        var claims = result.Principal?.Identities.FirstOrDefault()
            ?.Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
                
        return Json(new { Tokens = tokens, Claims = claims });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
}
