namespace Raven.Auth.Application.Models;

public class RegisterUserDatabaseDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string RegisterIp { get; set; } 
    public DateTime RegisterTimestamp { get; set; } 
}