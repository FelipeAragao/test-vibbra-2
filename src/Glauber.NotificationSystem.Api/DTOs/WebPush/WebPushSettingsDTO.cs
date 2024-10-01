namespace Glauber.NotificationSystem.Api.DTOs.WebPush;

public class WebPushSettingsDTO
{
    public SiteDTO Site { get; set; }
    public AllowNotificationDTO AllowNotification { get; set; }
    public WelcomeNotificationDTO WelcomeNotification { get; set; }
}
