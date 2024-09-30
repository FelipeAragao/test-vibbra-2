namespace Glauber.NotificationSystem.Api.DTOs.WebPush;

public class WebPushSettingsDTO : SettingsDTO
{
    public SiteDTO Site { get; set; }
    public AllowNotificationDTO AllowNotification { get; set; }
    public WelcomeNotificationDTO WelcomeNotification { get; set; }
}
