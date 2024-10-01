namespace Glauber.NotificationSystem.Api.DTOs.Email;

public class EmailSettingsDTO
{
    public ServerDTO Server { get; set; }
    public SenderDTO Sender { get; set; }
    public List<EmailTemplateDTO> EmailTemplates { get; set; }
}
