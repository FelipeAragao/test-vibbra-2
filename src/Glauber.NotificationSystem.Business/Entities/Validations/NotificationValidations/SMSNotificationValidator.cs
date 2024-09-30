using System.Text.RegularExpressions;
using FluentValidation;

namespace Glauber.NotificationSystem.Business.Entities.Validations.NotificationValidations;

public partial class SMSNotificationValidator : AbstractValidator<SMSNotification>
{
    public SMSNotificationValidator()
    {
        RuleFor(s => s.MessageText)
        .NotEmpty().WithMessage("Please provide a message text");

        RuleForEach(s => s.PhoneNumber)
        .NotEmpty().WithMessage("Please provide at least a phone number")
        .Must(BeValidPhoneNumber).WithMessage("Please provide a valid phone number");
    }

    private bool BeValidPhoneNumber(string phoneNumber)
    {
        var match = PhoneNumberRegex().Match(phoneNumber);
        return match.Success;
    }

    [GeneratedRegex("([(][0-9]{2}[)])?[0-9]{4,5}-?[0-9]{4}")]
    private static partial Regex PhoneNumberRegex();
}