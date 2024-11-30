using Microsoft.AspNetCore.Identity;

namespace Raven.Auth.Models;

public class AppUser : IdentityUser
{
    public string? GoogleId { get; set; }
    public bool IsExternalAccountLinked { get; set; }
}