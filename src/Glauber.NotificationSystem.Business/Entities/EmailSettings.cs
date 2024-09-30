using Glauber.NotificationSystem.Business.Entities.Base;
using Glauber.NotificationSystem.Business.Entities.Email;

namespace Glauber.NotificationSystem.Business.Entities;

public class EmailSettings : BaseSettings
{
    public Server Server { get; set; }
    public Sender Sender { get; set; }
    public List<EmailTemplate> EmailTemplates { get; set; }
}
