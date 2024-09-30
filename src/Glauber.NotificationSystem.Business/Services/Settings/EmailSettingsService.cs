using FluentResults;
using FluentValidation;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Entities.Validations.SettingsValidations;
using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Business.Interfaces.Repository.SettingsRepository;
using Glauber.NotificationSystem.Business.Interfaces.Service.SettingsService;

namespace Glauber.NotificationSystem.Business.Services.Settings;

public class EmailSettingsService(IEmailSettingsRepository settingsRepository, IAppRepository appRepository) : SettingsService<EmailSettings>(settingsRepository, appRepository), IEmailSettingsService
{
    public override AbstractValidator<EmailSettings> Validator => new EmailSettingsValidator();

    public override async Task<Result<bool>> GetChannelStatusAsync(int appId)
    {
        var app = await _appRepository.GetAppWithActiveChannelsAsync(appId);
        return Result.Ok(app.ActiveChannels.Email);
    }

    public override async Task<Result> ToggleChannelStatusAsync(int appId)
    {
        var app = await _appRepository.GetAppWithActiveChannelsAsync(appId);
        app.ActiveChannels.Email = !app.ActiveChannels.Email;
        return Result.OkIf(
            isSuccess: await _appRepository.UpdateAsync(app), 
            error: "Failed to toggle channel status"
        );
    }
}
