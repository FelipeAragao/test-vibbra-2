namespace Glauber.NotificationSystem.Api.DTOs.WebPush;

public class WebPushNotificationDTO
{
    public List<string> AudienceSegments { get; set; }
    public string MessageTitle { get; set; }
    public string MessageText { get; set; }
    public string IconUrl { get; set; }
    public string RedirectUrl { get; set; }
}

public class WebPushNotificationDetailDTO : NotificationDTO
{
    public List<string> AudienceSegments { get; set; }
    public string MessageTitle { get; set; }
    public string MessageText { get; set; }
    public string IconUrl { get; set; }
    public string RedirectUrl { get; set; }
}
