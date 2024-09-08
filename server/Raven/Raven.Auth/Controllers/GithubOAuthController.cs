using AspNet.Security.OAuth.GitHub;

namespace Raven.Auth.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.Google;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("oauth/github")]
    public class GithubOAuthController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            var redirectUrl = Url.Action("RespCallback", "GithubOAuth", null, Request.Scheme);
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GitHubAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("respcallback")]
        public async Task<IActionResult> RespCallback()
        {
            var result = await HttpContext.AuthenticateAsync(GitHubAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded) return RedirectToAction("Login");
            
            var tokens = result.Properties.GetTokens();
            var authenticationTokens = tokens.ToList();
            var accessToken = authenticationTokens?.FirstOrDefault(t => t.Name == "access_token")?.Value;
            var idToken = authenticationTokens?.FirstOrDefault(t => t.Name == "id_token")?.Value;
                
            var claims = result.Principal?.Identities.FirstOrDefault()
                ?.Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
                
            return Json(new { AccessToken = accessToken, IdToken = idToken, Claims = claims });
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}