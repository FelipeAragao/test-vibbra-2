using AutoMapper;
using Glauber.NotificationSystem.Api.DTOs;
using Glauber.NotificationSystem.Api.DTOs.Email;
using Glauber.NotificationSystem.Api.DTOs.SMS;
using Glauber.NotificationSystem.Api.DTOs.WebPush;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Entities.Email;
using Glauber.NotificationSystem.Business.Entities.SMS;
using Glauber.NotificationSystem.Business.Entities.WebPush;

namespace Glauber.NotificationSystem.Api.Configuration.AutoMapping;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        // App
        CreateMap<AppDTO, App>();
        CreateMap<App, AppResponseDTO>().ForMember(
            dest => dest.AppId, opt => opt.MapFrom(source => source.Id));
        CreateMap<ActiveChannelsDTO, ActiveChannels>().ReverseMap();

        // Settings
        CreateMap<EmailSettings, EmailSettingsDTO>().ReverseMap();
        CreateMap<SMSSettings, SMSSettingsDTO>().ReverseMap();
        CreateMap<WebPushSettings, WebPushSettingsDTO>().ReverseMap();

        // Email Settings
        CreateMap<Server, ServerDTO>().ReverseMap();
        CreateMap<Sender, SenderDTO>().ReverseMap();
        CreateMap<EmailTemplate, EmailTemplateDTO>().ReverseMap();

        // SMS Settings
        CreateMap<SMSProvider, SMSProviderDTO>().ReverseMap();

        // WebPush Settings
        CreateMap<Site, SiteDTO>().ReverseMap();
        CreateMap<AllowNotification, AllowNotificationDTO>().ReverseMap();
        CreateMap<WelcomeNotification, WelcomeNotificationDTO>().ReverseMap();

        // Notifications
        CreateMap<EmailNotificationDTO, EmailNotification>();
        CreateMap<EmailNotification, EmailNotificationDetailDTO>().ForMember(
            dest => dest.NotificationId, opt => opt.MapFrom(source => source.Id));
        CreateMap<SMSNotificationDTO, SMSNotification>();
        CreateMap<SMSNotification, SMSNotificationDetailDTO>().ForMember(
            dest => dest.NotificationId, opt => opt.MapFrom(source => source.Id));
        CreateMap<WebPushNotificationDTO, WebPushNotification>();
        CreateMap<WebPushNotification, WebPushNotificationDetailDTO>().ForMember(
            dest => dest.NotificationId, opt => opt.MapFrom(source => source.Id));
    }
}