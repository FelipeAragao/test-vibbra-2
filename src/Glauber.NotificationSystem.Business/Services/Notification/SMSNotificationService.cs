using FluentValidation;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Entities.Validations.NotificationValidations;
using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Business.Interfaces.Repository.NotificationRepository;
using Glauber.NotificationSystem.Business.Interfaces.Service.NotificationService;

namespace Glauber.NotificationSystem.Business.Services.Notification;

public class SMSNotificationService(ISMSNotificationRepository notificationRepository, IAppRepository appRepository) : NotificationService<SMSNotification>(notificationRepository, appRepository), ISMSNotificationService
{
    public override AbstractValidator<SMSNotification> Validator => new SMSNotificationValidator();
}
