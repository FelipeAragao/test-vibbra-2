using FluentResults;
using Glauber.NotificationSystem.Business.Entities.Base;
using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Business.Interfaces.Repository.SettingsRepository;
using Glauber.NotificationSystem.Business.Interfaces.Service;

namespace Glauber.NotificationSystem.Business.Services;

public abstract class SettingsService<TSettings>(ISettingsRepository<TSettings> settingsRepository, IAppRepository appRepository) : BaseService<TSettings>, ISettingsService<TSettings> where TSettings : BaseSettings
{
    private readonly ISettingsRepository<TSettings> _settingsRepository = settingsRepository;
    protected readonly IAppRepository _appRepository = appRepository;

    public async Task<Result> AddChannelSettingsAsync(int appId, TSettings settings)
    {
        var validationResult = Validate(settings);
        if (!validationResult.IsValid)
        {
            return Result
                .Fail("Validation failed")
                .WithErrors(GetValidationErrors(validationResult));
        }

        settings.AppId = appId;

        return Result.OkIf(
            isSuccess: await _settingsRepository.AddAsync(settings),
            error: "Failed to add settings"
        );
    }

    public virtual async Task<Result<TSettings>> GetChannelSettingsByAppAsync(int appId)
    {
        var channelSettings = await _settingsRepository.GetChannelSettingsByAppAsync(appId);
        return Result.Ok(channelSettings);
    }

    public void Dispose() => _settingsRepository.Dispose();

    public abstract Task<Result> ToggleChannelStatusAsync(int appId);

    public abstract Task<Result<bool>> GetChannelStatusAsync(int appId);
}