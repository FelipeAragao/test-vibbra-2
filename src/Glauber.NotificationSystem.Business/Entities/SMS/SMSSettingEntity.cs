using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Entities.SMS;

public class SMSSettingEntity : BaseEntity
{
    public int SMSSettingsId { get; set; }
    public SMSSettings SMSSettings { get; set; }
}
