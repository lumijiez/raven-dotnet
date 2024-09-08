namespace Raven.Auth.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.Google;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("oauth/google")]
    public class GoogleOAuthController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            var redirectUrl = Url.Action("Response", "GoogleOAuth", null, Request.Scheme);
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("response")]
        public async Task<IActionResult> Response()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

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