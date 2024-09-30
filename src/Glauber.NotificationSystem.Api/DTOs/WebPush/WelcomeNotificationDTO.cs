namespace Glauber.NotificationSystem.Api.DTOs.WebPush;

public class WelcomeNotificationDTO
{
    public string MessageTitle { get; set; }
    public string MessageText { get; set; }
    public bool EnableUrlRedirect { get; set; }
    public string? UrlRedirect { get; set; }
}
