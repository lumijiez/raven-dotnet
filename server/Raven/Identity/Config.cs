using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: "message-service", "Messaging API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
            {
               ClientId = "client",
               AllowedGrantTypes = GrantTypes.ClientCredentials,
               ClientSecrets =
               {
                   new Secret("secret".Sha256())
               },
               AllowedScopes = { "message-service" }
            },
            new Client
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
                    "message-service"
                }
            }
        };
}