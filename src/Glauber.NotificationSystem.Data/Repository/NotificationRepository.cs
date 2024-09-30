using Glauber.NotificationSystem.Business.Entities.Base;
using Glauber.NotificationSystem.Business.Interfaces.Repository.NotificationRepository;
using Glauber.NotificationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Glauber.NotificationSystem.Data.Repository;

public class NotificationRepository<TNotification>(NotificationSystemDbContext context) : BaseRepository<TNotification>(context), INotificationRepository<TNotification> where TNotification : BaseNotification, new()
{
    public async Task<TNotification> GetNotificationAsync(int appId, int notificationId) 
        => await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(n => 
                n.AppId == appId && 
                n.Id == notificationId) 
            ?? new TNotification();

    public async Task<IEnumerable<TNotification>> GetNotificationsByDateAsync(int appId, DateTime initDate, DateTime endDate) 
        => await DbSet
            .Where(n => 
                n.AppId == appId && 
                n.SendDate >= initDate && 
                n.SendDate <= endDate)
            .ToListAsync();
}