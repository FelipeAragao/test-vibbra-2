using System.Text.RegularExpressions;
using FluentValidation;

namespace Glauber.NotificationSystem.Business.Entities.Validations.SettingsValidations;

public partial class WebpushSettingsValidator : AbstractValidator<WebPushSettings>
{
    public WebpushSettingsValidator()
    {
        RuleFor(w => w.Site)
        .NotEmpty().WithMessage("Please provide valid site information for the webpush settings")
        .ChildRules(site =>
        {
            site.RuleFor(ws => ws.Name)
            .NotEmpty().WithMessage("Please provide the name for the site");

            site.RuleFor(ws => ws.Address)
            .NotEmpty().WithMessage("Please provide the site address")
            .Must(BeValidUri).WithMessage("Please provide a valid site address format");

            site.RuleFor(ws => ws.UrlIcon)
            .NotEmpty().WithMessage("Please provide a valid url icon address")
            .Must(BeValidUri).WithMessage("Please provide a valid format for url icon");
        });

        RuleFor(w => w.AllowNotification)
        .NotEmpty().WithMessage("Please provide a valid setting for AllowNotification")
        .ChildRules(allowNotification =>
        {
            allowNotification.RuleFor(wa => wa.MessageText)
            .NotEmpty().WithMessage("Please provide a valid message text for allow notification");

            allowNotification.RuleFor(wa => wa.AllowButtonText)
            .NotEmpty().WithMessage("Please provide a valid message text for the 'Allow' button");

            allowNotification.RuleFor(wa => wa.DenyButtonText)
            .NotEmpty().WithMessage("Please provide a valid message text for the 'Deny' button");
        });

        RuleFor(w => w.WelcomeNotification)
        .NotEmpty().WithMessage("Please provide a valid setting for WelcomeNotification")
        .ChildRules(welcomeNotification =>
        {
            welcomeNotification.RuleFor(ww => ww.MessageText)
            .NotEmpty().WithMessage("Please provide a valid message text for the WelcomeNotification settings");

            welcomeNotification.RuleFor(ww => ww.MessageTitle)
            .NotEmpty().WithMessage("Please provide a valid message text for the WelcomeNotification settings");

            welcomeNotification.RuleFor(ww => ww.EnableUrlRedirect)
            .Must(BeValidUrlRedirect).WithMessage("Please provide either 0 or 1 value for url redirect");

            When(ww => !string.IsNullOrWhiteSpace(ww.WelcomeNotification.UrlRedirect), () =>
            {
                RuleFor(ww => ww.WelcomeNotification.UrlRedirect)
                .Must(BeValidUri).WithMessage("If provided, please make sure that url redirect is in a valid format");
            });
        });
    }

    private bool BeValidUrlRedirect(int value)
    {
        return value == 0 || value == 1;
    }

    private bool BeValidUri(string? uri)
    {
        if (string.IsNullOrWhiteSpace(uri))
            return false;
        
        var match = SiteUriRegex().Match(uri);
        return match.Success;
    }

    [GeneratedRegex(@"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)")]
    private static partial Regex SiteUriRegex();
}
