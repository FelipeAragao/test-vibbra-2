using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Entities;

public class App : BaseEntity
{
    public string AppName { get; set; }
    public string AppToken { get; private set; } = GenerateAppToken();
    public ActiveChannels ActiveChannels { get; set; } = new ActiveChannels();

    /* ER Relation */
    public EmailSettings? EmailSettings { get; set; }
    public SMSSettings? SMSSettings { get; set; }
    public WebPushSettings? WebPushSettings { get; set; }

    public IEnumerable<EmailNotification>? EmailNotification { get; set; }
    public IEnumerable<SMSNotification>? SMSNotification { get; set; }
    public IEnumerable<WebPushNotification>? WebPushNotification { get; set; }

    private static string GenerateAppToken()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var token = new string(Enumerable.Repeat(chars, 30)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        return token;
    }
}
