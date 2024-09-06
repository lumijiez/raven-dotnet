using Raven.Auth.Application.Models;
using Raven.Auth.Domain.Services;

namespace Raven.Auth.Application.Services;

public class AuthAppService(AuthService authService)
{
    public void RegisterUser(RegisterUserDTO dto)
    {
        authService.RegisterUser(dto.Username, dto.Password, dto.Email);
    }
}