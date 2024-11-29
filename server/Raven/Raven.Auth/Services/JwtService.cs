using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Raven.Auth.Interfaces;
using Raven.Auth.Models;

namespace Raven.Auth.Services;

public class JwtService : IJwtService
{
    private readonly RSA _rsaPrivateKey;
    private readonly RSA _rsaPublicKey;
    private readonly IConfiguration _configuration;
    
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
        _rsaPrivateKey = RSA.Create();
        _rsaPublicKey = RSA.Create();

        ImportOrGenerateKeys();
    }
    
    private void ImportOrGenerateKeys()
    {
        _rsaPrivateKey.KeySize = 2048;
        _rsaPublicKey.ImportParameters(_rsaPrivateKey.ExportParameters(false));
    }
    
    public string GenerateToken(AppUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var signingCredentials = new SigningCredentials(
            new RsaSecurityKey(_rsaPrivateKey), 
            SecurityAlgorithms.RsaSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public string GetPublicKey()
    {
        var publicKeyBytes = _rsaPublicKey.ExportRSAPublicKey();
        return Convert.ToBase64String(publicKeyBytes);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new RsaSecurityKey(_rsaPublicKey)
        };

        return tokenHandler.ValidateToken(token, validationParameters, out _);
    }
}