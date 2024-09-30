using FluentValidation;

namespace Glauber.NotificationSystem.Business.Entities.Validations.NotificationValidations;

public class EmailNotificationValidator : AbstractValidator<EmailNotification>
{
    public EmailNotificationValidator()
    {
        RuleFor(e => e.MessageText)
        .NotEmpty().WithMessage("Please provide a valid message text");

        RuleForEach(e => e.ReceiverEmail)
        .NotEmpty().WithMessage("Please provide at least one email receiver");

        RuleFor(e => e.EmailTemplateName)
        .NotEmpty().WithMessage("Please provide a valid email template name");
    }
}