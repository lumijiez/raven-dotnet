namespace Raven.Auth.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } 
    public string HashedPassword { get; set; }
    public string Email { get; set; }
    public string RegisterIp { get; set; } 
    public DateTime RegisterTimestamp { get; set; } 
}
