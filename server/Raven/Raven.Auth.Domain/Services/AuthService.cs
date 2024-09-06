﻿using Raven.Auth.Data.Entities;
using Raven.Auth.Domain.Interfaces;

namespace Raven.Auth.Domain.Services;

public class AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher)
{
    public void RegisterUser(string username, string password, string email)
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
            Email = email
        };

        authRepository.Add(user);
    }
}