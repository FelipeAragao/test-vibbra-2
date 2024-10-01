using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Interfaces.Repository.SettingsRepository;
using Glauber.NotificationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Glauber.NotificationSystem.Data.Repository;

public class WebPushSettingsRepository(NotificationSystemDbContext context) : SettingsRepository<WebPushSettings>(context), IWebpushSettingsRepository
{
    public override async Task<WebPushSettings> GetChannelSettingsByAppAsync(int appId)
    {
        return await DbSet.AsNoTracking()
            .Include(w => w.Site)
            .Include(w => w.AllowNotification)
            .Include(w => w.WelcomeNotification)
            .SingleOrDefaultAsync(w => w.AppId == appId);
    }
}
