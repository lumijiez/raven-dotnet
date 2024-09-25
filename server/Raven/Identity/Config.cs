using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new()
        {
            UserClaims = ["email"]
        }
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new(name: "message", "Messaging API")
    ];

    public static IEnumerable<Client> Clients =>
    [
        new()
            {
               ClientId = "client",
               AllowedGrantTypes = GrantTypes.ClientCredentials,
               ClientSecrets =
               {
                   new Secret("secret".Sha256())
               },
               AllowedScopes = { "message" }
            },
            new()
        {
                ClientId = "web",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RedirectUris = { "https://localhost:5002/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "message"
                },
                AlwaysSendClientClaims = true
            }
    ];
}