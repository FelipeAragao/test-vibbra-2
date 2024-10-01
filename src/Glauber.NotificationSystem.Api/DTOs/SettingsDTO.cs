using Glauber.NotificationSystem.Api.DTOs.Email;
using Glauber.NotificationSystem.Api.DTOs.SMS;
using Glauber.NotificationSystem.Api.DTOs.WebPush;

namespace Glauber.NotificationSystem.Api.DTOs;

public record SettingsEmailResponse(EmailSettingsDTO Settings);
public record SettingsSMSResponse(SMSSettingsDTO Settings);
public record SettingsWebPushResponse(WebPushSettingsDTO Settings);
