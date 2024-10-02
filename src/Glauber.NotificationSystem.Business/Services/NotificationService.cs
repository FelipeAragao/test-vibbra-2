using FluentResults;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Entities.Base;
using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Business.Interfaces.Repository.NotificationRepository;
using Glauber.NotificationSystem.Business.Interfaces.Service;

namespace Glauber.NotificationSystem.Business.Services;

public abstract class NotificationService<TNotification>(INotificationRepository<TNotification> notificationRepository, IAppRepository appRepository) : BaseService<TNotification>, INotificationService<TNotification> where TNotification : BaseNotification
{
    private readonly IAppRepository _appRepository = appRepository;
    private readonly INotificationRepository<TNotification> _notificationRepository = notificationRepository;

    public App App { get; set; }

    public async Task<Result> CreateNotificationAsync(int appId, TNotification notification)
    {
        if (await AppDoesNotExist(appId))
        {
            return Result.Fail("No app was found with the provided Id");
        }

        if (!IsChannelInactive())
        {
            return Result.Fail("Channel is not active");
        }
        
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
        if (await AppDoesNotExist(appId))
        {
            return Result.Fail("No app was found with the provided Id");
        }

        if (IsChannelInactive())
        {
            return Result.Fail("Channel is not active");
        }

        var notification = await _notificationRepository.GetNotificationAsync(appId, notificationId);
        if (notification == null)
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
        if (await AppDoesNotExist(appId))
        {
            return Result.Fail("No app was found with the provided Id");
        }

        if (IsChannelInactive())
        {
            return Result.Fail("Channel is not active");
        }

        var notifications = await _notificationRepository.GetNotificationsByDateAsync(appId, initDate, endDate);
        if (!notifications.Any())
        {
            return Result.Fail("There are no notifications for the specified parameters!");
        }

        return Result.Ok(notifications);
    }

    public void Dispose() => _notificationRepository.Dispose();

    protected abstract bool IsChannelInactive();

    protected async Task<bool> AppDoesNotExist(int appId)
    {
        App = await _appRepository.GetAppWithActiveChannelsAsync(appId);
        return App == null;
    }
}
