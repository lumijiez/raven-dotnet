using Raven.Auth.Domain.Entities;
using Raven.Auth.Domain.Interfaces;

namespace Raven.Auth.Domain.Services;

public class AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher)
{
    public void RegisterUser(string username, string password, string email, string registerIp, DateTime registerTimestamp)
    {
        if (authRepository.GetByUsername(username) != null)
        {
            throw new ArgumentException("Username already exists");
        }

        var hashedPassword = passwordHasher.HashPassword(password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            HashedPassword = hashedPassword,
            Email = email,
            RegisterIp = registerIp,
            RegisterTimestamp = registerTimestamp
        };

        authRepository.Add(user);
    }

    public bool LoginUser(string username, string password)
    {
        var user = authRepository.GetByUsername(username);
        
        if (user == null) return false;
        
        var hashedPassword = passwordHasher.HashPassword(password);

        return hashedPassword == user.HashedPassword;
    }
}