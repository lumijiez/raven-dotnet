using Raven.Auth.Application.Models;
using Raven.Auth.Domain.Services;

namespace Raven.Auth.Application.Services;

public class AuthAppService(AuthService authService)
{
    public void RegisterUser(RegisterUserDatabaseDTO dto)
    {
        authService.RegisterUser(dto.Username, dto.Password, dto.Email, dto.RegisterIp, dto.RegisterTimestamp);
    }
    
    public bool LoginUser(LoginUserDTO dto)
    {
        return authService.LoginUser(dto.Username, dto.Password);
    }
}