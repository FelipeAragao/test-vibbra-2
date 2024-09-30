namespace Glauber.NotificationSystem.Api.DTOs.Email;

public class EmailSettingsDTO : SettingsDTO
{
    public ServerDTO Server { get; set; }
    public SenderDTO Sender { get; set; }
    public List<EmailTemplateDTO> EmailTemplates { get; set; }
}
