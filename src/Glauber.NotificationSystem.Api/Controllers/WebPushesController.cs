using AutoMapper;
using FluentResults;
using Glauber.NotificationSystem.Api.DTOs;
using Glauber.NotificationSystem.Api.DTOs.WebPush;
using Glauber.NotificationSystem.Api.Responses;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Interfaces.Service;
using Glauber.NotificationSystem.Business.Interfaces.Service.NotificationService;
using Glauber.NotificationSystem.Business.Interfaces.Service.SettingsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Glauber.NotificationSystem.Api.Controllers;

[Route("apps/{appId:int}/[controller]")]
[ApiController]
[Authorize]
public class WebPushesController(IAppService appService, IWebPushSettingsService webpushSettingsService, IWebPushNotificationService webpushNotificationService, IMapper mapper) : MainController
{
    private readonly IAppService _appService = appService;
    private readonly IWebPushSettingsService _webpushSettingsService = webpushSettingsService;
    private readonly IWebPushNotificationService _webpushNotificationService = webpushNotificationService;
    private readonly IMapper _mapper = mapper;

    [HttpGet("settings")]
    public async Task<ActionResult> GetSettings(int appId)
    {
        var operationResult = await _webpushSettingsService.GetChannelSettingsByAppAsync(appId);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        return Ok(new SettingsWebPushResponse(_mapper.Map<WebPushSettingsDTO>(operationResult.Value)));
    }

    [HttpPost("settings")]
    public async Task<ActionResult<WebPushSettingsDTO>> AddSettings([FromRoute]int appId, [FromBody] WebPushSettingsDTO WebPushSettingsDTO)
    {
        var smsSettings = _mapper.Map<WebPushSettings>(WebPushSettingsDTO);
        var operationResult = await _webpushSettingsService.AddChannelSettingsAsync(appId, smsSettings);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }

        return CreatedAtAction(nameof(GetSettings), new { appId = smsSettings.AppId }, WebPushSettingsDTO);
    }

    [HttpPut("settings")]
    public async Task<ActionResult> ToggleChannel(int appId)
    {
        var operationResult = await _webpushSettingsService.ToggleChannelStatusAsync(appId);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        var result = await _webpushSettingsService.GetChannelStatusAsync(appId);
        return Ok(new ChannelStatus(
            Convert.ToInt32(!result.Value),
            Convert.ToInt32(result.Value)
        ));
    }

    [HttpGet("notifications")]
    public async Task<ActionResult> GetNotifications([FromRoute]int appId, [FromQuery]DateTime initDate, [FromQuery]DateTime endDate)
    {        
        var operationResult = await _webpushNotificationService.GetNotificationsByDateAsync(appId, initDate, endDate);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        return Ok(operationResult.Value.Select(n => new NotificationHistory(n.Id, n.SendDate)));
    }

    [HttpGet("notifications/{notificationId:int}")]
    public async Task<ActionResult<WebPushNotificationDetailDTO>> GetNotification(int appId, int notificationId)
    {
        var operationResult = await _webpushNotificationService.GetNotificationAsync(appId, notificationId);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        return Ok(_mapper.Map<WebPushNotificationDetailDTO>(operationResult.Value));
    }

    [HttpPost("notifications")]
    public async Task<ActionResult> CreateNotification([FromRoute]int appId, [FromBody] WebPushNotificationDTO notificationDTO)
    {
        var notification = _mapper.Map<WebPushNotification>(notificationDTO);
        
        var operationResult = await _webpushNotificationService.CreateNotificationAsync(appId, notification);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }

        return CreatedAtAction(nameof(GetNotification), new { appId, notificationId = notification.Id }, new CreatedNotification(notification.Id));
    }
}