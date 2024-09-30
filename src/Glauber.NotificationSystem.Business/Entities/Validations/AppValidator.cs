using FluentValidation;

namespace Glauber.NotificationSystem.Business.Entities.Validations;

public class AppValidator : AbstractValidator<App>
{
    public AppValidator()
    {
        RuleFor(a => a.AppName)
            .NotEmpty().WithMessage("Please provide a valid app name");
    }
}