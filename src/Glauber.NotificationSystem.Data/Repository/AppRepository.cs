using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Glauber.NotificationSystem.Data.Repository;

public class AppRepository(NotificationSystemDbContext context) : BaseRepository<App>(context), IAppRepository
{
    public async Task<App> GetAppWithActiveChannelsAsync(int appId) => await DbSet
        .AsNoTracking()
        .Include(a => a.ActiveChannels)
        .FirstOrDefaultAsync(a => a.Id == appId);
}
