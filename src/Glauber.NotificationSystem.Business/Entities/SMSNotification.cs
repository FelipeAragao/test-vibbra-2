using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Entities;

public class SMSNotification : BaseNotification
{
    public List<string> PhoneNumber { get; set; }
    public string MessageText { get; set; }
}
