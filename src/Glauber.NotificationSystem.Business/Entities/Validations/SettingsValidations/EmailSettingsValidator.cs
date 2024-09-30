using System.Text.RegularExpressions;
using FluentValidation;

namespace Glauber.NotificationSystem.Business.Entities.Validations.SettingsValidations;

public partial class EmailSettingsValidator : AbstractValidator<EmailSettings>
{
    public EmailSettingsValidator()
    {
        RuleFor(e => e.Server)
        .NotEmpty().WithMessage("Please provide an email server")
        .ChildRules(server => 
        {
            server.RuleFor(es => es.UserLogin)
            .NotEmpty().WithMessage("Please provide a valid user login for server");

            server.RuleFor(es => es.UserPassword)
            .NotEmpty().WithMessage("Please provide a valid user password for server");
        });

        RuleFor(e => e.Sender)
        .NotEmpty().WithMessage("Please provide an email sender")
        .ChildRules(sender =>
        {
            sender.RuleFor(es => es.Email)
            .NotEmpty().WithMessage("Please provide an email address for the sender")
            .Must(BeValidEmail).WithMessage("Please provide a valid email format for the sender");

            sender.RuleFor(es => es.Name)
            .NotEmpty().WithMessage("Please provide a name for the sender");
        });

        RuleFor(e => e.EmailTemplates)
        .NotEmpty().WithMessage("Please provide at least one email template");

        RuleForEach(e => e.EmailTemplates).ChildRules(template =>
        {
            template.RuleFor(et => et.Name)
            .NotEmpty().WithMessage("Please provide a valid template name");

            template.RuleFor(et => et.Uri)
            .NotEmpty().WithMessage("Please provide an uri")
            .Must(BeValidUri).WithMessage("Please provide an uri in a valid format");
        });
    }

    private bool BeValidUri(string uri)
    {
        var match = EmailTemplateUriRegex().Match(uri);
        return match.Success;
    }

    private bool BeValidEmail(string email)
    {
        var match = EmailFormatRegex().Match(email);
        return match.Success;
    }

    [GeneratedRegex(@"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)")]
    private static partial Regex EmailTemplateUriRegex();
    [GeneratedRegex(@"^[\w\-\.]+@([\w-]+\.)+[\w-]{2,}$")]
    private static partial Regex EmailFormatRegex();
}
