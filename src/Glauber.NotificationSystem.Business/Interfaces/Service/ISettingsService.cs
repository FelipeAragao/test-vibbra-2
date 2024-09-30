using FluentResults;
using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Interfaces.Service;

public interface ISettingsService<TSettings> : IDisposable where TSettings : BaseSettings
{
    // Create
    Task<Result> AddChannelSettingsAsync(int appId, TSettings settings);
    // Read
    Task<Result<TSettings>> GetChannelSettingsByAppAsync(int appId);

    Task<Result<bool>> GetChannelStatusAsync(int appId);

    Task<Result> ToggleChannelStatusAsync(int appId);
}
