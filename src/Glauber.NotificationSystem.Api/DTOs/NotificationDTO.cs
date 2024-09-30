namespace Glauber.NotificationSystem.Api.DTOs;

public class NotificationDTO
{
    public int NotificationId { get; set; }
    public DateTime SendDate { get; set; }
}

public record CreatedNotification(int NotificationId);
public record NotificationHistory(int NotificationId, DateTime SendDate);