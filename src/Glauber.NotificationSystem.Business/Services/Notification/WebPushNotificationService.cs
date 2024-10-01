using FluentValidation;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Entities.Validations.NotificationValidations;
using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Business.Interfaces.Repository.NotificationRepository;
using Glauber.NotificationSystem.Business.Interfaces.Service.NotificationService;

namespace Glauber.NotificationSystem.Business.Services.Notification;

public class WebPushNotificationService(IWebpushNotificationRepository notificationRepository, IAppRepository appRepository) : NotificationService<WebPushNotification>(notificationRepository, appRepository), IWebPushNotificationService
{
    public override AbstractValidator<WebPushNotification> Validator => new WebpushNotificationValidator();

    protected override bool IsChannelActive()
    {
        return App.ActiveChannels.WebPush;
    }
}
