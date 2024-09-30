namespace Glauber.NotificationSystem.Api.DTOs.Email;

public class ServerDTO
{
    public string SmtpName { get; set; }
    public string SmtpPort { get; set; }
    public string UserLogin { get; set; }
    public string UserPassword { get; set; }
}