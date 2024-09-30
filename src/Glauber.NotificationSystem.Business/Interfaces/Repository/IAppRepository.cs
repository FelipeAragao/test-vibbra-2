using Glauber.NotificationSystem.Business.Entities;

namespace Glauber.NotificationSystem.Business.Interfaces.Repository;

public interface IAppRepository : IBaseRepository<App>
{
    Task<App> GetAppWithActiveChannelsAsync(int appId);
}
