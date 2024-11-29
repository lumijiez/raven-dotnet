using System.Security.Claims;
using Raven.Auth.Models;

namespace Raven.Auth.Interfaces;

public interface IJwtService
{
    string GenerateToken(AppUser user);
    ClaimsPrincipal ValidateToken(string token);
}