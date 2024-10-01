using AutoMapper;
using Glauber.NotificationSystem.Api.DTOs;
using Glauber.NotificationSystem.Api.DTOs.Email;
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
public class EmailsController(IAppService appService, IEmailSettingsService emailSettingsService, IEmailNotificationService emailNotificationService, IMapper mapper) : MainController
{
    private readonly IAppService _appService = appService;
    private readonly IEmailSettingsService _emailSettingsService = emailSettingsService;
    private readonly IEmailNotificationService _emailNotificationService = emailNotificationService;
    private readonly IMapper _mapper = mapper;

    [HttpGet("settings")]
    public async Task<ActionResult> GetSettings(int appId)
    {
        var operationResult = await _emailSettingsService.GetChannelSettingsByAppAsync(appId);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        
        return Ok(new SettingsEmailResponse(_mapper.Map<EmailSettingsDTO>(operationResult.Value)));
    }

    [HttpPost("settings")]
    public async Task<ActionResult<EmailSettingsDTO>> AddSettings([FromRoute]int appId, [FromBody] EmailSettingsDTO emailSettingsDTO)
    {
        var emailSettings = _mapper.Map<EmailSettings>(emailSettingsDTO);
        var operationResult = await _emailSettingsService.AddChannelSettingsAsync(appId, emailSettings);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }

        return CreatedAtAction(nameof(GetSettings), new { appId = emailSettings.AppId }, emailSettingsDTO);
    }

    [HttpPut("settings")]
    public async Task<ActionResult> ToggleChannel(int appId)
    {
        var operationResult = await _emailSettingsService.ToggleChannelStatusAsync(appId);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        var result = await _emailSettingsService.GetChannelStatusAsync(appId);
        return Ok(new ChannelStatus(
            Convert.ToInt32(!result.Value),
            Convert.ToInt32(result.Value)
        ));
    }

    [HttpGet("notifications")]
    public async Task<ActionResult> GetNotifications([FromRoute]int appId, [FromQuery]DateTime initDate, [FromQuery]DateTime endDate)
    {        
        var operationResult = await _emailNotificationService.GetNotificationsByDateAsync(appId, initDate, endDate);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        return Ok(operationResult.Value.Select(n => new NotificationHistory(n.Id, n.SendDate)));
    }

    [HttpGet("notifications/{notificationId:int}")]
    public async Task<ActionResult<EmailNotificationDetailDTO>> GetNotification(int appId, int notificationId)
    {
        var operationResult = await _emailNotificationService.GetNotificationAsync(appId, notificationId);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }
        return Ok(_mapper.Map<EmailNotificationDetailDTO>(operationResult.Value));
    }

    [HttpPost("notifications")]
    public async Task<ActionResult> CreateNotification([FromRoute]int appId, [FromBody] EmailNotificationDTO notificationDTO)
    {
        var notification = _mapper.Map<EmailNotification>(notificationDTO);
        
        var operationResult = await _emailNotificationService.CreateNotificationAsync(appId, notification);
        if (operationResult.IsFailed)
        {
            return FormatBadRequestResponse(operationResult.Errors);
        }

        return CreatedAtAction(nameof(GetNotification), new { appId, notificationId = notification.Id }, new CreatedNotification(notification.Id));
    }
}
