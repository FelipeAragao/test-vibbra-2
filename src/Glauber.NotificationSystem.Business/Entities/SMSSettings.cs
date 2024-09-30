using Glauber.NotificationSystem.Business.Entities.Base;
using Glauber.NotificationSystem.Business.Entities.SMS;

namespace Glauber.NotificationSystem.Business.Entities;

public class SMSSettings : BaseSettings
{
    public SMSProvider SMSProvider { get; set; }
}
