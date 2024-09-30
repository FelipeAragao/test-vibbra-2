using FluentValidation;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Entities.Validations.NotificationValidations;
using Glauber.NotificationSystem.Business.Interfaces.Repository.NotificationRepository;
using Glauber.NotificationSystem.Business.Interfaces.Service.NotificationService;

namespace Glauber.NotificationSystem.Business.Services.Notification;

public class WebPushNotificationService(IWebpushNotificationRepository notificationRepository) : NotificationService<WebPushNotification>(notificationRepository), IWebPushNotificationService
{
    public override AbstractValidator<WebPushNotification> Validator => new WebpushNotificationValidator();
}
