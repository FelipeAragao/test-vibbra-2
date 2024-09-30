using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Entities.WebPush;

public class WebPushSettingEntity : BaseEntity
{
    public int WebPushSettingsId { get; set; }
    public WebPushSettings WebPushSettings { get; set; }
}
