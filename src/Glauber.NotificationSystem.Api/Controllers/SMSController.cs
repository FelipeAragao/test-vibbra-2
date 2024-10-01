using AutoMapper;
using FluentResults;
using Glauber.NotificationSystem.Api.DTOs;
using Glauber.NotificationSystem.Api.DTOs.SMS;
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
public class SMSController(IAppService appService, ISMSSettingsService smsSettingsService, ISMSNotificationService smsNotificationService, IMapper mapper) : MainController
{
    private readonly IAppService _appService = appService;
    private readonly ISMSSettingsService _smsSettingsService = smsSettingsService;
    private readonly ISMSNotificationService _smsNotificationService = smsNotificationService;
    private readonly IMapper _mapper = mapper;

    [HttpGet("settings")]
    public async Task<ActionResult> GetSettings(int appId)
    {
        var operationResult = await _smsSettingsService.GetChannelSettingsByAppAsync(appId);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        return Ok(new SettingsSMSResponse(_mapper.Map<SMSSettingsDTO>(operationResult.Value)));
    }

    [HttpPost("settings")]
    public async Task<ActionResult<SMSSettingsDTO>> AddSettings([FromRoute]int appId, [FromBody] SMSSettingsDTO SMSSettingsDTO)
    {
        var smsSettings = _mapper.Map<SMSSettings>(SMSSettingsDTO);
        var operationResult = await _smsSettingsService.AddChannelSettingsAsync(appId, smsSettings);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }

        return CreatedAtAction(nameof(GetSettings), new { appId = smsSettings.AppId }, SMSSettingsDTO);
    }

    [HttpPut("settings")]
    public async Task<ActionResult> ToggleChannel(int appId)
    {
        var operationResult = await _smsSettingsService.ToggleChannelStatusAsync(appId);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        var result = await _smsSettingsService.GetChannelStatusAsync(appId);
        return Ok(new ChannelStatus(
            Convert.ToInt32(!result.Value),
            Convert.ToInt32(result.Value)
        ));
    }

    [HttpGet("notifications")]
    public async Task<ActionResult> GetNotifications([FromRoute]int appId, [FromQuery]DateTime initDate, [FromQuery]DateTime endDate)
    {        
        var operationResult = await _smsNotificationService.GetNotificationsByDateAsync(appId, initDate, endDate);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        return Ok(operationResult.Value.Select(n => new NotificationHistory(n.Id, n.SendDate)));
    }

    [HttpGet("notifications/{notificationId:int}")]
    public async Task<ActionResult<SMSNotificationDetailDTO>> GetNotification(int appId, int notificationId)
    {
        var operationResult = await _smsNotificationService.GetNotificationAsync(appId, notificationId);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        return Ok(_mapper.Map<SMSNotificationDetailDTO>(operationResult.Value));
    }

    [HttpPost("notifications")]
    public async Task<ActionResult> CreateNotification([FromRoute]int appId, [FromBody] SMSNotificationDTO notificationDTO)
    {
        var notification = _mapper.Map<SMSNotification>(notificationDTO);
        
        var operationResult = await _smsNotificationService.CreateNotificationAsync(appId, notification);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }

        return CreatedAtAction(nameof(GetNotification), new { appId, notificationId = notification.Id }, new CreatedNotification(notification.Id));
    }
}