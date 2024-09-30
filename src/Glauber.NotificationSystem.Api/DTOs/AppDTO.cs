namespace Glauber.NotificationSystem.Api.DTOs;

public class AppDTO
{
    public string AppName { get; set; }
}

public class AppResponseDTO
{
    public int AppId { get; set; }
    public string AppName { get; set; }
    public string AppToken { get; set; }
    public ActiveChannelsDTO ActiveChannels { get; set; }
}

public record CreatedApp(int AppId, string AppToken);
