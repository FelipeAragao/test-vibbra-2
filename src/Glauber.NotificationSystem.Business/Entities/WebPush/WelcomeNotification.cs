namespace Glauber.NotificationSystem.Business.Entities.WebPush;

public class WelcomeNotification : WebPushSettingEntity
{
    public string MessageTitle { get; set; }
    public string MessageText { get; set; }
    public int EnableUrlRedirect { get; set; }
    public string? UrlRedirect { get; set; }
}
