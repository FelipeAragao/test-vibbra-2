namespace Glauber.NotificationSystem.Api.DTOs.SMS;

public class SMSNotificationDTO
{
    public List<string> PhoneNumber { get; set; }
    public string MessageText { get; set; }
}

public class SMSNotificationDetailDTO : NotificationDTO
{
    public List<string> PhoneNumber { get; set; }
    public string MessageText { get; set; }
}
