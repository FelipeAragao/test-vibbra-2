using FluentValidation;

namespace Glauber.NotificationSystem.Business.Entities.Validations.SettingsValidations;

public class SMSSettingsValidator : AbstractValidator<SMSSettings>
{
    public SMSSettingsValidator()
    {
        RuleFor(s => s.SMSProvider)
        .NotEmpty().WithMessage("Please provide the SMS provider")
        .ChildRules(provider =>
        {
            provider.RuleFor(sp => sp.Login)
            .NotEmpty().WithMessage("Please provide a valid login for the SMS provider");

            provider.RuleFor(sp => sp.Name)
            .NotEmpty().WithMessage("Please provide a valid name for the SMS provider");

            provider.RuleFor(sp => sp.Password)
            .NotEmpty().WithMessage("Please provide a valid password for the SMS provider");
        });
    }
}