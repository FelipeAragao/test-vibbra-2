using Microsoft.AspNetCore.Identity;

namespace Glauber.NotificationSystem.Api.Data;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyAddress { get; set; }
}
