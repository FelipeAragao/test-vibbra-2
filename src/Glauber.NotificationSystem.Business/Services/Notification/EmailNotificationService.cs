using FluentValidation;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Entities.Validations.NotificationValidations;
using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Business.Interfaces.Repository.NotificationRepository;
using Glauber.NotificationSystem.Business.Interfaces.Service.NotificationService;

namespace Glauber.NotificationSystem.Business.Services.Notification;

public class EmailNotificationService(INotificationRepository<EmailNotification> notificationRepository, IAppRepository appRepository) : NotificationService<EmailNotification>(notificationRepository, appRepository), IEmailNotificationService
{
    public override AbstractValidator<EmailNotification> Validator => new EmailNotificationValidator();
}
