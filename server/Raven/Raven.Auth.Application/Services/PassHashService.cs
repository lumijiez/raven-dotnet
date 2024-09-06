using Raven.Auth.Domain.Interfaces;

namespace Raven.Auth.Application.Services;

public class PassHashService : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}