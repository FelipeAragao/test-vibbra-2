using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Interfaces.Repository.NotificationRepository;
using Glauber.NotificationSystem.Data.Context;

namespace Glauber.NotificationSystem.Data.Repository;

public class WebPushNotificationRepository(NotificationSystemDbContext context) : NotificationRepository<WebPushNotification>(context), IWebpushNotificationRepository
{
}
