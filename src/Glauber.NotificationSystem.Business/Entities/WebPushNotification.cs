using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Entities;

public class WebPushNotification : BaseNotification
{
    public List<string> AudienceSegments { get; set; }
    public string MessageTitle { get; set; }
    public string MessageText { get; set; }
    public string IconUrl { get; set; }
    public string RedirectUrl { get; set; }
}
