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
        if (await AppDoesNotExist(appId))
        {
            return Result.Fail("No app was found with the provided Id");
        }
        var validationResult = Validate(settings);
        if (!validationResult.IsValid)
        {
            return Result
                .Fail("Validation failed")
                .WithErrors(GetValidationErrors(validationResult));
        }

        settings.AppId = appId;
        var currentSettings = await _settingsRepository.GetChannelSettingsByAppAsync(appId);

        if (currentSettings != null)
        {
            currentSettings = settings;
            return Result.OkIf(
                isSuccess: await _settingsRepository.UpdateAsync(currentSettings),
                error: "Failed to change settings");
        }

        return Result.OkIf(
            isSuccess: await _settingsRepository.AddAsync(settings),
            error: "Failed to add settings"
        );
    }

    public virtual async Task<Result<TSettings>> GetChannelSettingsByAppAsync(int appId)
    {
        if (await AppDoesNotExist(appId))
        {
            return Result.Fail("No app was found with the provided Id");
        }
        var channelSettings = await _settingsRepository.GetChannelSettingsByAppAsync(appId);
        if (channelSettings == null)
        {
            return Result.Fail("There are no settings for the provided app");
        }
        return Result.Ok(channelSettings);
    }

    public void Dispose() => _settingsRepository.Dispose();

    public abstract Task<Result> ToggleChannelStatusAsync(int appId);

    public abstract Task<Result<bool>> GetChannelStatusAsync(int appId);

    protected async Task<bool> AppDoesNotExist(int appId)
    {
        return await _appRepository.GetAppWithActiveChannelsAsync(appId) == null;
    }
}