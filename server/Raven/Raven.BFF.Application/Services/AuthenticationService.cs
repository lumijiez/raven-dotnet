using IdentityModel.Client;
using Raven.BFF.Application.Models;
using Raven.BFF.Application.Models.Requests;
using Raven.BFF.Domain.Settings;

namespace Raven.BFF.Application.Services;

public class AuthenticationService(IHttpClientFactory httpClientFactory, OpenIDSettings openId)
{
    public async Task<Result> LoginUserAsync(RavenLoginRequest request)
    {
        var client = httpClientFactory.CreateClient();

        var disco = await client.GetDiscoveryDocumentAsync(openId.IssuerUrl);
        if (disco.IsError)
        {
            return new Result
            {
                Error = true,
                Message = "Couldn't get discovery document"
            };
        }
        
        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = openId.ClientId,
            ClientSecret = openId.ClientSecret,
            UserName = request.Username,
            Password = request.Password,
            Scope = "openid profile email message"
        });

        if (tokenResponse.IsError)
        {
            return new Result
            {
                Error = true,
                Message = "Authentication failed"
            };
        }

        return new Result
        {
            Success = true,
            Data = tokenResponse.Json
        };
    }
}