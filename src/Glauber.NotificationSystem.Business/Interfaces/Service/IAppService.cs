using FluentResults;
using Glauber.NotificationSystem.Business.Entities;

namespace Glauber.NotificationSystem.Business.Interfaces.Service;

public interface IAppService : IDisposable
{
    // Create
    Task<Result> AddAppAsync(App app);
    // Read
    Task<Result<IEnumerable<App>>> GetAppsAsync();
    Task<Result<App>> GetAppAsync(int appId);
}
