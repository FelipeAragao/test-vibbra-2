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

namespace Glauber.NotificationSystem.Api.Controllers
{
    [Route("api/apps/{appId:int}/[controller]")]
    [ApiController]
    [Authorize]
    public class WebPushesController(IAppService appService, IWebPushSettingsService webpushSettingsService, IWebPushNotificationService webpushNotificationService, IMapper mapper) : MainController
    {
        private readonly IAppService _appService = appService;
        private readonly IWebPushSettingsService _webpushSettingsService = webpushSettingsService;
        private readonly IWebPushNotificationService _webpushNotificationService = webpushNotificationService;
        private readonly IMapper _mapper = mapper;

        //[ClaimsAuthorize("Settings", "Get")]
        [HttpGet("settings")]
        public async Task<ActionResult> GetSettings(int appId)
        {
            var operationResult = await _webpushSettingsService.GetChannelSettingsByAppAsync(appId);
            if (operationResult.IsFailed)
            {
                return FormatResponse(operationResult.ToResult());
            }
            if (operationResult.Value == null)
            {
                return NotFound();
            }
            return Ok(new SettingsResponse(_mapper.Map<WebPushSettingsDTO>(operationResult.Value)));
        }

        //[ClaimsAuthorize("Settings", "Add")]
        [HttpPost("settings")]
        public async Task<ActionResult<WebPushSettingsDTO>> AddSettings([FromRoute]int appId, [FromBody] WebPushSettingsDTO webpushSettingsDTO)
        {
            var webpushSettings = _mapper.Map<WebPushSettings>(webpushSettingsDTO);
            var operationResult = await _webpushSettingsService.AddChannelSettingsAsync(appId, webpushSettings);
            if (operationResult.IsFailed)
            {
                return FormatResponse(operationResult);
            }

            return CreatedAtAction(nameof(GetSettings), new { appId = webpushSettings.AppId }, webpushSettings);
        }

        //[ClaimsAuthorize("Settings", "Toggle")]
        [HttpPut("settings")]
        public async Task<ActionResult> ToggleChannel(int appId)
        {
            var operationResult = await _webpushSettingsService.ToggleChannelStatusAsync(appId);
            if (operationResult.IsFailed)
            {
                return FormatResponse(operationResult);
            }
            var result = await _webpushSettingsService.GetChannelStatusAsync(appId);
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
            
            var notifications = await _webpushNotificationService.GetNotificationsByDateAsync(appId, initDate, endDate);
            return Ok(notifications.Value.Select(n => new NotificationHistory(n.Id, n.SendDate)));
        }

        //[ClaimsAuthorize("Notification", "GetDetails")]
        [HttpGet("notifications/{notificationId:int}")]
        public async Task<ActionResult<WebPushNotificationDetailDTO>> GetNotification(int appId, int notificationId)
        {
            if (!await IsChannelActive(appId))
            {
                return FormatResponse(Result.Fail("Channel is not active"));
            }
            var notification = await _webpushNotificationService.GetNotificationAsync(appId, notificationId);
            return Ok(_mapper.Map<WebPushNotificationDetailDTO>(notification.Value));
        }

        //[ClaimsAuthorize("Notification", "Create")]
        [HttpPost("notifications")]
        public async Task<ActionResult> CreateNotification([FromRoute]int appId, [FromBody] WebPushNotificationDTO notificationDTO)
        {
            if (!await IsChannelActive(appId))
            {
                return FormatResponse(Result.Fail("Channel is not active"));
            }

            var notification = _mapper.Map<WebPushNotification>(notificationDTO);

            await _webpushNotificationService.CreateNotificationAsync(appId, notification);
            return CreatedAtAction(nameof(GetNotification), new { appId, notificationId = notification.Id }, new CreatedNotification(notification.Id));
        }

        private async Task<bool> IsChannelActive(int appId)
        {
            var result = await _webpushSettingsService.GetChannelStatusAsync(appId);
            return result.Value;
        }
    }
}