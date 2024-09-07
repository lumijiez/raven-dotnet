using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Raven.Auth.Domain.Services;

using System.Security.Claims;
using System.Text;

public class JwtTokenHelper(IConfiguration config)
{
    private readonly string _key = config["Jwt:Key"] ?? throw new KeyNotFoundException();
    private readonly string _issuer = config["Jwt:Issuer"] ?? throw new KeyNotFoundException();
    private readonly string _audience = config["Jwt:Audience"] ?? throw new KeyNotFoundException();
    private readonly int _expiresInMinutes = int.Parse(config["Jwt:ExpiresInMinutes"] ?? throw new KeyNotFoundException());
    private readonly int _refreshTokenExpiresInDays = int.Parse(config["Jwt:RefreshTokenExpiresInDays"] ?? throw new KeyNotFoundException());

    public string GenerateToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_expiresInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key))
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        return principal;
    }
}
