using AutoMapper;
using FluentResults;
using Glauber.NotificationSystem.Api.DTOs;
using Glauber.NotificationSystem.Api.DTOs.Email;
using Glauber.NotificationSystem.Api.Responses;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Interfaces.Service;
using Glauber.NotificationSystem.Business.Interfaces.Service.NotificationService;
using Glauber.NotificationSystem.Business.Interfaces.Service.SettingsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Glauber.NotificationSystem.Api.Controllers
{
    [Route("api/apps/{appId:int}/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailsController(IAppService appService, IEmailSettingsService emailSettingsService, IEmailNotificationService emailNotificationService, IMapper mapper) : MainController
    {
        private readonly IAppService _appService = appService;
        private readonly IEmailSettingsService _emailSettingsService = emailSettingsService;
        private readonly IEmailNotificationService _emailNotificationService = emailNotificationService;
        private readonly IMapper _mapper = mapper;

        //[ClaimsAuthorize("Settings", "Get")]
        [HttpGet("settings")]
        public async Task<ActionResult> GetSettings(int appId)
        {
            var result = await _emailSettingsService.GetChannelSettingsByAppAsync(appId);
            if (result.Value == null)
            {
                return NotFound();

            }
            return Ok(new SettingsResponse(_mapper.Map<EmailSettingsDTO>(result.Value)));
        }

        //[ClaimsAuthorize("Settings", "Add")]
        [HttpPost("settings")]
        public async Task<ActionResult<EmailSettingsDTO>> AddSettings([FromRoute]int appId, [FromBody] EmailSettingsDTO emailSettingsDTO)
        {
            var emailSettings = _mapper.Map<EmailSettings>(emailSettingsDTO);
            await _emailSettingsService.AddChannelSettingsAsync(appId, emailSettings);

            return CreatedAtAction(nameof(GetSettings), new { appId = emailSettings.AppId }, emailSettings);
        }

        //[ClaimsAuthorize("Settings", "Toggle")]
        [HttpPut("settings")]
        public async Task<ActionResult> ToggleChannel(int appId)
        {
            var operationResult = await _emailSettingsService.ToggleChannelStatusAsync(appId);
            if (operationResult.IsFailed)
            {
                return FormatResponse(operationResult);
            }
            var result = await _emailSettingsService.GetChannelStatusAsync(appId);
            return Ok(new ChannelStatus(
                Convert.ToInt32(!result.Value),
                Convert.ToInt32(result.Value)
            ));
        }

        //[ClaimsAuthorize("Notification", "GetHistory")]
        [HttpGet("notifications")]
        public async Task<ActionResult> GetNotifications([FromRoute]int appId, [FromQuery]DateTime initDate, [FromQuery]DateTime endDate)
        {
            if (!await IsChannelActive(appId))
            {
                return FormatResponse(Result.Fail("Channel is not active"));
            }
            
            var notifications = await _emailNotificationService.GetNotificationsByDateAsync(appId, initDate, endDate);
            return Ok(notifications.Value.Select(n => new NotificationHistory(n.Id, n.SendDate)));
        }

        //[ClaimsAuthorize("Notification", "GetDetails")]
        [HttpGet("notifications/{notificationId:int}")]
        public async Task<ActionResult<EmailNotificationDetailDTO>> GetNotification(int appId, int notificationId)
        {
            if (!await IsChannelActive(appId))
            {
                return FormatResponse(Result.Fail("Channel is not active"));
            }
            var notification = await _emailNotificationService.GetNotificationAsync(appId, notificationId);
            return Ok(_mapper.Map<EmailNotificationDetailDTO>(notification.Value));
        }

        //[ClaimsAuthorize("Notification", "Create")]
        [HttpPost("notifications")]
        public async Task<ActionResult> CreateNotification([FromRoute]int appId, [FromBody] EmailNotificationDTO notificationDTO)
        {
            if (!await IsChannelActive(appId))
            {
                return FormatResponse(Result.Fail("Channel is not active"));
            }

            var notification = _mapper.Map<EmailNotification>(notificationDTO);

            await _emailNotificationService.CreateNotificationAsync(appId, notification);
            return CreatedAtAction(nameof(GetNotification), new { appId, notificationId = notification.Id }, new CreatedNotification(notification.Id));
        }

        private async Task<bool> IsChannelActive(int appId)
        {
            var result = await _emailSettingsService.GetChannelStatusAsync(appId);
            return result.Value;
        }
    }
}
