using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Interfaces.Repository.SettingsRepository;
using Glauber.NotificationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Glauber.NotificationSystem.Data.Repository;

public class EmailSettingsRepository(NotificationSystemDbContext context) : SettingsRepository<EmailSettings>(context), IEmailSettingsRepository
{
    public override async Task<EmailSettings> GetChannelSettingsByAppAsync(int appId)
    {
        return await DbSet
            .Include(e => e.Sender)
            .Include(e => e.Server)
            .Include(e => e.EmailTemplates)
            .SingleOrDefaultAsync(e => e.AppId == appId);
    }
}
