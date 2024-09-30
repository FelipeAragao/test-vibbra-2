using Glauber.NotificationSystem.Business.Interfaces.Repository;
using Glauber.NotificationSystem.Business.Interfaces.Repository.NotificationRepository;
using Glauber.NotificationSystem.Business.Interfaces.Repository.SettingsRepository;
using Glauber.NotificationSystem.Business.Interfaces.Service;
using Glauber.NotificationSystem.Business.Interfaces.Service.NotificationService;
using Glauber.NotificationSystem.Business.Interfaces.Service.SettingsService;
using Glauber.NotificationSystem.Business.Services;
using Glauber.NotificationSystem.Business.Services.Notification;
using Glauber.NotificationSystem.Business.Services.Settings;
using Glauber.NotificationSystem.Data.Context;
using Glauber.NotificationSystem.Data.Repository;

namespace Glauber.NotificationSystem.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<NotificationSystemDbContext>();
        
        services.AddScoped<IWebpushSettingsRepository, WebPushSettingsRepository>();
        services.AddScoped<IEmailSettingsRepository, EmailSettingsRepository>();
        services.AddScoped<ISMSSettingsRepository, SMSSettingsRepository>();

        services.AddScoped<IWebpushNotificationRepository, WebPushNotificationRepository>();
        services.AddScoped<IEmailNotificationRepository, EmailNotificationRepository>();
        services.AddScoped<ISMSNotificationRepository, SMSNotificationRepository>();

        services.AddScoped<IAppRepository, AppRepository>();

        services.AddScoped<IWebpushSettingsRepository, WebPushSettingsRepository>();
        services.AddScoped<IEmailSettingsRepository, EmailSettingsRepository>();
        services.AddScoped<ISMSSettingsRepository, SMSSettingsRepository>();

        services.AddScoped<IWebPushNotificationService, WebPushNotificationService>();
        services.AddScoped<IEmailNotificationService, EmailNotificationService>();
        services.AddScoped<ISMSNotificationService, SMSNotificationService>();

        services.AddScoped<IWebPushSettingsService, WebPushSettingsService>();
        services.AddScoped<IEmailSettingsService, EmailSettingsService>();
        services.AddScoped<ISMSSettingsService, SMSSettingsService>();

        services.AddScoped<IAppService, AppService>();
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //services.AddScoped<IUser, AspNetUser>();
        
        return services;
    }
}
