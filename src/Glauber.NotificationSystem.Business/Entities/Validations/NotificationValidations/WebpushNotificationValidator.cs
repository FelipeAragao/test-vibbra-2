using System.Text.RegularExpressions;
using FluentValidation;

namespace Glauber.NotificationSystem.Business.Entities.Validations.NotificationValidations;

public partial class WebpushNotificationValidator : AbstractValidator<WebPushNotification>
{
    public WebpushNotificationValidator()
    {
        RuleForEach(w => w.AudienceSegments)
        .NotEmpty().WithMessage("Please provide at least one valid audience segment");

        RuleFor(w => w.MessageTitle)
        .NotEmpty().WithMessage("Please provide a message title");
        
        RuleFor(w => w.MessageText)
        .NotEmpty().WithMessage("Please provide a message text");
        
        RuleFor(w => w.IconUrl)
        .NotEmpty().WithMessage("Please provide an icon url")
        .Must(BeValidUrl).WithMessage("Please provide a valid url information");
        
        RuleFor(w => w.RedirectUrl)
        .NotEmpty().WithMessage("Please provide a url for redirection")
        .Must(BeValidUrl).WithMessage("Please provide a valid url information");;
    }

    private bool BeValidUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;
        
        var match = SiteUrlRegex().Match(url);
        return match.Success;
    }

    [GeneratedRegex(@"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)")]
    private static partial Regex SiteUrlRegex();
}