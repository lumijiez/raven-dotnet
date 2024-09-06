namespace Raven.Auth.Domain.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);
}