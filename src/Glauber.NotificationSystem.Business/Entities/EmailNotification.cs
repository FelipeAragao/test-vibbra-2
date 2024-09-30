using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Entities;

public class EmailNotification : BaseNotification
{
    public List<string> ReceiverEmail { get; set; }
    public string EmailTemplateName { get; set; }
    public string MessageText { get; set; }
    public bool Received { get; set; }
    public bool Opened { get; set; }
    public bool Clicked { get; set; }
}
