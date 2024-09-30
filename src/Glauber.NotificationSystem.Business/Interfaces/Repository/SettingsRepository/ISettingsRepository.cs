using Glauber.NotificationSystem.Business.Entities.Base;

namespace Glauber.NotificationSystem.Business.Interfaces.Repository.SettingsRepository;

public interface ISettingsRepository<TSettings> : IBaseRepository<TSettings> where TSettings : BaseSettings
{
    // Read
    Task<TSettings> GetChannelSettingsByAppAsync(int appId);
}
