using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Interfaces.Repository.SettingsRepository;
using Glauber.NotificationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Glauber.NotificationSystem.Data.Repository;

public class SMSSettingsRepository(NotificationSystemDbContext context) : SettingsRepository<SMSSettings>(context), ISMSSettingsRepository
{
    public override async Task<SMSSettings> GetChannelSettingsByAppAsync(int appId)
    {
        return await DbSet.AsNoTracking()
            .Include(s => s.SMSProvider)
            .SingleOrDefaultAsync(s => s.AppId == appId);
    }
}