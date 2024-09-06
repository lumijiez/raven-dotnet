using System.ComponentModel.DataAnnotations.Schema;

namespace Raven.Auth.Domain.Entities;

public class User
{
    
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("username")]
    public string Username { get; set; } 
    
    [Column("hashedpassword")]
    public string HashedPassword { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("registerip")]
    public string RegisterIp { get; set; } 
    
    [Column("registertimestamp")]
    public DateTime RegisterTimestamp { get; set; } 
}
