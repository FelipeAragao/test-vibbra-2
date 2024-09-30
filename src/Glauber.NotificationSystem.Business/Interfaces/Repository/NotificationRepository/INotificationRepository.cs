using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Interfaces.Repository.NotificationRepository;

public interface INotificationRepository<TNotification> : IBaseRepository<TNotification> where TNotification : BaseNotification
{
    Task<IEnumerable<TNotification>> GetNotificationsByDateAsync(int appId, DateTime initDate, DateTime endDate);
    Task<TNotification> GetNotificationAsync(int appId, int notificationId);
}
