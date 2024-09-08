using Raven.Auth.Domain.Entities;

namespace Raven.Auth.Domain.Interfaces;

public interface IAuthRepository
{
    User? GetByUsername(string username);
    User? GetByEmail(string email);
    string? GetHashByUsername(string username);
    void Add(User user);
}