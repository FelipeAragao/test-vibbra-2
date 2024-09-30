using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Services;

public abstract class BaseService<TEntity> where TEntity : BaseEntity
{
    public abstract AbstractValidator<TEntity> Validator { get; }

    protected ValidationResult Validate(TEntity entity)
    {
        return Validator.Validate(entity);
    }
    protected static List<IError> GetValidationErrors(ValidationResult validationResult)
    {
        List<IError> errors = [];
        validationResult.Errors.ForEach(e =>
        {
            errors.Add(new Error(e.ErrorMessage));
        });
        return errors;
    }
}
