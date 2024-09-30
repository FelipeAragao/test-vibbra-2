using FluentResults;
using Glauber.NotificationSystem.Business.Entities.Base;
using Glauber.NotificationSystem.Business.Interfaces.Repository.NotificationRepository;
using Glauber.NotificationSystem.Business.Interfaces.Service;

namespace Glauber.NotificationSystem.Business.Services;

public abstract class NotificationService<TNotification>(INotificationRepository<TNotification> notificationRepository) : BaseService<TNotification>, INotificationService<TNotification> where TNotification : BaseNotification
{
    private readonly INotificationRepository<TNotification> _notificationRepository = notificationRepository;

    public async Task<Result> CreateNotificationAsync(int appId, TNotification notification)
    {
        var validationResult = Validate(notification);
        if (!validationResult.IsValid)
        {
            return Result
                .Fail("Validation failed!")
                .WithErrors(GetValidationErrors(validationResult));
        }

        notification.AppId = appId;
        
        return Result.OkIf(
            isSuccess: await _notificationRepository.AddAsync(notification),
            error: "Failed to add new notification"
        );
    }

    public async Task<Result<TNotification>> GetNotificationAsync(int appId, int notificationId)
    {
        var notification = await _notificationRepository.GetNotificationAsync(appId, notificationId);
        if (notification.AppId != appId)
        {
            return Result.Fail("There is no notification for the informed app.");
        }
        else if (notification.Id != notificationId)
        {
            return Result.Fail("There is no notification with the provided ID.");            
        }

        return Result.Ok(notification);
    }

    public async Task<Result<IEnumerable<TNotification>>> GetNotificationsByDateAsync(int appId, DateTime initDate, DateTime endDate)
    {
        var notifications = await _notificationRepository.GetNotificationsByDateAsync(appId, initDate, endDate);
        if (!notifications.Any())
        {
            return Result.Fail("There are no notifications for the specified parameters!");
        }

        return Result.Ok(notifications);
    }

    public void Dispose() => _notificationRepository.Dispose();
}
