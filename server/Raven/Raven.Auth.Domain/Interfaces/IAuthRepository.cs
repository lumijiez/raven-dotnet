using Raven.Auth.Domain.Entities;

namespace Raven.Auth.Domain.Interfaces;

public interface IAuthRepository
{
    User? GetByUsername(string username);
    User? GetByEmail(string email);
    void Add(User user);
}