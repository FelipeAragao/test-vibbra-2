using Glauber.NotificationSystem.Business.Entities.Base;
using Glauber.NotificationSystem.Business.Entities.WebPush;

namespace Glauber.NotificationSystem.Business.Entities;

public class WebPushSettings : BaseSettings
{
    public Site Site { get; set; }
    public AllowNotification AllowNotification { get; set; }
    public WelcomeNotification WelcomeNotification { get; set; }
}
