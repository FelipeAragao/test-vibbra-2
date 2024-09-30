namespace Glauber.NotificationSystem.Api.DTOs.Email;

public class EmailNotificationDTO
{
    public List<string> ReceiverEmail { get; set; }
    public string EmailTemplateName { get; set; }
    public string MessageText { get; set; }
    public bool Received { get; set; }
    public bool Opened { get; set; }
    public bool Clicked { get; set; }
}

public class EmailNotificationDetailDTO : NotificationDTO
{
    public List<string> ReceiverEmail { get; set; }
    public string EmailTemplateName { get; set; }
    public string MessageText { get; set; }
    public bool Received { get; set; }
    public bool Opened { get; set; }
    public bool Clicked { get; set; }
}
