using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Raven.Auth.Dtos;
using Raven.Auth.Interfaces;
using Raven.Auth.Models;

namespace Raven.Auth.Services;

public class AuthService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IJwtService jwtService,
    IConfiguration configuration)
    : IAuthService
{
    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser() 
            { 
                UserName = registerDto.Email, 
                Email = registerDto.Email 
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new ApplicationException(
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            var token = jwtService.GenerateToken(user);
            return new AuthResponseDto 
            { 
                Token = token, 
                UserId = user.Id 
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var token = jwtService.GenerateToken(user);
            return new AuthResponseDto 
            { 
                Token = token, 
                UserId = user.Id 
            };
        }

        public async Task<AuthResponseDto> GoogleLoginAsync(string accessToken)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={accessToken}");
    
            if (!response.IsSuccessStatusCode)
            {
                throw new UnauthorizedAccessException("Invalid Google access token");
            }

            var userInfo = await response.Content.ReadFromJsonAsync<GoogleUserInfo>();

            if (userInfo == null)
            {
                throw new UnauthorizedAccessException("Could not retrieve user information");
            }

            var user = await userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                user = new AppUser() 
                { 
                    UserName = userInfo.Email, 
                    Email = userInfo.Email,
                    GoogleId = userInfo.Id,
                    IsExternalAccountLinked = true
                };
                await userManager.CreateAsync(user);
            }
            else if (string.IsNullOrEmpty(user.GoogleId))
            {
                user.GoogleId = userInfo.Id;
                user.IsExternalAccountLinked = true;
                await userManager.UpdateAsync(user);
            }

            var token = jwtService.GenerateToken(user);
            return new AuthResponseDto 
            { 
                Token = token, 
                UserId = user.Id 
            };
        }
        
        public async Task<AppUser?> FindUserByEmailAsync(string email) =>
            await userManager.FindByEmailAsync(email);

        public async Task CreateUserAsync(AppUser user)
        {
            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                throw new ApplicationException(
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }
        }
        
        public async Task UpdateUserAsync(AppUser user)
        {
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ApplicationException(
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }
        }

        
        public string GenerateJwtToken(AppUser user) => jwtService.GenerateToken(user);
}