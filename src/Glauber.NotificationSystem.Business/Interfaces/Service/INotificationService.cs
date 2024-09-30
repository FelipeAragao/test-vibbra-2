using FluentResults;
using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Interfaces.Service;

public interface INotificationService<TNotification> : IDisposable where TNotification : BaseNotification
{
    // Create
    Task<Result> CreateNotificationAsync(int appId, TNotification notification);
    // Read
    Task<Result<IEnumerable<TNotification>>> GetNotificationsByDateAsync(int appId, DateTime initDate, DateTime endDate);
    Task<Result<TNotification>> GetNotificationAsync(int appId, int notificationId);
}
