using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Entities.Email;

public abstract class EmailSettingEntity : BaseEntity
{
    public int EmailSettingsId { get; set; }
    public EmailSettings EmailSettings { get; set; }
}
