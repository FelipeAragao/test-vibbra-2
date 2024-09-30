namespace Glauber.NotificationSystem.Business.Entities.WebPush;

public class AllowNotification : WebPushSettingEntity
{
    public string MessageText { get; set; }
    public string AllowButtonText { get; set; }
    public string DenyButtonText { get; set; }
}
