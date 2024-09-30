using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Entities.Base;
using Glauber.NotificationSystem.Business.Interfaces.Repository.SettingsRepository;
using Glauber.NotificationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Glauber.NotificationSystem.Data.Repository;

public abstract class SettingsRepository<TSettings>(NotificationSystemDbContext context) : BaseRepository<TSettings>(context), ISettingsRepository<TSettings> where TSettings : BaseSettings
{
    public virtual async Task<TSettings> GetChannelSettingsByAppAsync(int appId)
    {
        return await DbSet.SingleOrDefaultAsync(c => c.AppId == appId);
    }
}