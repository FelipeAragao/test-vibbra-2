using FluentResults;
using FluentValidation;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Entities.Validations.SettingsValidations;
using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Business.Interfaces.Repository.SettingsRepository;
using Glauber.NotificationSystem.Business.Interfaces.Service.SettingsService;

namespace Glauber.NotificationSystem.Business.Services.Settings;

public class SMSSettingsService(ISMSSettingsRepository settingsRepository, IAppRepository appRepository) : SettingsService<SMSSettings>(settingsRepository, appRepository), ISMSSettingsService
{
    public override AbstractValidator<SMSSettings> Validator => new SMSSettingsValidator();

    public override async Task<Result<bool>> GetChannelStatusAsync(int appId)
    {
        var app = await _appRepository.GetAppWithActiveChannelsAsync(appId);
        if (app == null)
        {
            return Result.Fail("No app was found with the provided Id");
        }
        return Result.Ok(app.ActiveChannels.SMS);
    }

    public override async Task<Result> ToggleChannelStatusAsync(int appId)
    {
        var app = await _appRepository.GetAppWithActiveChannelsAsync(appId);
        if (app == null)
        {
            return Result.Fail("No app was found with the provided Id");
        }
        app.ActiveChannels.SMS = !app.ActiveChannels.SMS;
        return Result.OkIf(
            isSuccess: await _appRepository.UpdateAsync(app), 
            error: "Failed to toggle channel status"
        );
    }
}
