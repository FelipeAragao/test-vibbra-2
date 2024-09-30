namespace Glauber.NotificationSystem.Api.DTOs;

public class ActiveChannelsDTO
{
    public bool WebPush { get; set; }
    public bool Email { get; set; }
    public bool SMS { get; set; }    
}