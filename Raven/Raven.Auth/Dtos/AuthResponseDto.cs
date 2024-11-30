namespace Raven.Auth.Dtos;

public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public string UserId { get; set; } = null!;
}