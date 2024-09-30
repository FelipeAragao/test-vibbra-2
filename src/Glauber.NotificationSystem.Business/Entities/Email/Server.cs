namespace Glauber.NotificationSystem.Business.Entities.Email;

public class Server : EmailSettingEntity
{
    public string SmtpName { get; set; }
    public string SmtpPort { get; set; }
    public string UserLogin { get; set; }
    public string UserPassword { get; set; }
}
