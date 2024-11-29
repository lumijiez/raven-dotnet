using System.Security.Claims;
using Raven.Auth.Models;

namespace Raven.Auth.Interfaces;

public interface IJwtService
{
    string GenerateToken(AppUser user);
    string GetPublicKey();
    ClaimsPrincipal ValidateToken(string token);
}