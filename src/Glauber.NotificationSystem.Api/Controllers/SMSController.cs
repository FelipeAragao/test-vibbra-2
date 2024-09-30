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

namespace Glauber.NotificationSystem.Api.Controllers
{
    [Route("api/apps/{appId:int}/[controller]")]
    [ApiController]
    [Authorize]
    public class SMSController(IAppService appService, ISMSSettingsService smsSettingsService, ISMSNotificationService smsNotificationService, IMapper mapper) : MainController
    {
        private readonly IAppService _appService = appService;
        private readonly ISMSSettingsService _smsSettingsService = smsSettingsService;
        private readonly ISMSNotificationService _smsNotificationService = smsNotificationService;
        private readonly IMapper _mapper = mapper;

        //[ClaimsAuthorize("Settings", "Get")]
        [HttpGet("settings")]
        public async Task<ActionResult> GetSettings(int appId)
        {
            var result = await _smsSettingsService.GetChannelSettingsByAppAsync(appId);
            if (result.Value == null)
            {
                return NotFound();

            }
            return Ok(new SettingsResponse(_mapper.Map<SMSSettingsDTO>(result.Value)));
        }

        //[ClaimsAuthorize("Settings", "Add")]
        [HttpPost("settings")]
        public async Task<ActionResult<SMSSettingsDTO>> AddSettings([FromRoute]int appId, [FromBody] SMSSettingsDTO smsSettingsDTO)
        {
            var smsSettings = _mapper.Map<SMSSettings>(smsSettingsDTO);
            await _smsSettingsService.AddChannelSettingsAsync(appId, smsSettings);

            return CreatedAtAction(nameof(GetSettings), new { appId = smsSettings.AppId }, smsSettings);
        }

        //[ClaimsAuthorize("Settings", "Toggle")]
        [HttpPut("settings")]
        public async Task<ActionResult> ToggleChannel(int appId)
        {
            await _smsSettingsService.ToggleChannelStatusAsync(appId);
            var result = await _smsSettingsService.GetChannelStatusAsync(appId);
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
            
            var notifications = await _smsNotificationService.GetNotificationsByDateAsync(appId, initDate, endDate);
            return Ok(notifications.Value.Select(n => new NotificationHistory(n.Id, n.SendDate)));
        }

        //[ClaimsAuthorize("Notification", "GetDetails")]
        [HttpGet("notifications/{notificationId:int}")]
        public async Task<ActionResult<SMSNotificationDetailDTO>> GetNotification(int appId, int notificationId)
        {
            if (!await IsChannelActive(appId))
            {
                return FormatResponse(Result.Fail("Channel is not active"));
            }
            var notification = await _smsNotificationService.GetNotificationAsync(appId, notificationId);
            return Ok(_mapper.Map<SMSNotificationDetailDTO>(notification.Value));
        }

        //[ClaimsAuthorize("Notification", "Create")]
        [HttpPost("notifications")]
        public async Task<ActionResult> CreateNotification([FromRoute]int appId, [FromBody] SMSNotificationDTO notificationDTO)
        {
            if (!await IsChannelActive(appId))
            {
                return FormatResponse(Result.Fail("Channel is not active"));
            }

            var notification = _mapper.Map<SMSNotification>(notificationDTO);

            await _smsNotificationService.CreateNotificationAsync(appId, notification);
            return CreatedAtAction(nameof(GetNotification), new { appId, notificationId = notification.Id }, new CreatedNotification(notification.Id));
        }

        private async Task<bool> IsChannelActive(int appId)
        {
            var result = await _smsSettingsService.GetChannelStatusAsync(appId);
            return result.Value;
        }
    }
}