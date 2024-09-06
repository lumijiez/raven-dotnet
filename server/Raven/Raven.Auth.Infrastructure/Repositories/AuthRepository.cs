using Raven.Auth.Data.Entities;
using Raven.Auth.Domain.Interfaces;

namespace Raven.Auth.Data.Repositories;

public class AuthRepository(AuthDbContext context) : IAuthRepository
{
    public User? GetByUsername(string username)
    {
        return context.Users.SingleOrDefault(u => u.Username == username);
    }

    public User? GetByEmail(string email)
    {
        return context.Users.SingleOrDefault(u => u.Email == email);
    }

    public void Add(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
    }
}