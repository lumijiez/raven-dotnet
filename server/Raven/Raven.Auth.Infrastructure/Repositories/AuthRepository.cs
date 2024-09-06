using Raven.Auth.Domain.Entities;
using Raven.Auth.Domain.Interfaces;

namespace Raven.Auth.Infrastructure.Repositories;

public class AuthRepository(AuthDbContext context) : IAuthRepository
{
    public User? GetByUsername(string username)
    {
        return context.users.SingleOrDefault(u => u.Username == username);
    }

    public User? GetByEmail(string email)
    {
        return context.users.SingleOrDefault(u => u.Email == email);
    }

    public void Add(User user)
    {
        context.users.Add(user);
        context.SaveChanges();
    }
}