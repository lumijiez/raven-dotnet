using Raven.Auth.Dtos;
using Raven.Auth.Models;

namespace Raven.Auth.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<AuthResponseDto> GoogleLoginAsync(string googleToken);
    Task<AppUser?> FindUserByEmailAsync(string email);
    Task UpdateUserAsync(AppUser user);
    Task CreateUserAsync(AppUser user);
    string GenerateJwtToken(AppUser user);

}