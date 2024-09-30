using FluentResults;
using FluentValidation;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Entities.Validations;
using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Business.Interfaces.Service;

namespace Glauber.NotificationSystem.Business.Services;

public class AppService(IAppRepository appRepository) : BaseService<App>, IAppService
{
    private readonly IAppRepository _appRepository = appRepository;
    public override AbstractValidator<App> Validator { get => new AppValidator(); }
    public async Task<Result> AddAppAsync(App app)
    {
        var validationResult = Validate(app);
        if (!validationResult.IsValid)
        {
            return Result
                .Fail("Validation failed for app")
                .WithErrors(GetValidationErrors(validationResult));
        }
        
        return Result.OkIf(
            isSuccess: await _appRepository.AddAsync(app),
            error: "Failed to create a new app");
    }

    public async Task<Result<IEnumerable<App>>> GetAppsAsync()
    {
        var apps = await _appRepository.GetAllAsync();
        return Result.Ok(apps);
    }

    public async Task<Result<App>> GetAppAsync(int appId)
    {
        var app = await _appRepository.GetAppWithActiveChannelsAsync(appId);
        return Result.Ok(app);
    }

    public void Dispose() => _appRepository.Dispose();
}
