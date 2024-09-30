using Microsoft.AspNetCore.Mvc;
using Glauber.NotificationSystem.Business.Entities;
using Glauber.NotificationSystem.Business.Interfaces.Service;
using FluentResults.Extensions.AspNetCore;
using AutoMapper;
using Glauber.NotificationSystem.Api.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Glauber.NotificationSystem.Api.Controllers
{
    [Route("apps")]
    [ApiController]
    [Authorize]
    public class AppsController(IAppService appService, IMapper mapper) : MainController
    {
        private readonly IAppService _appService = appService;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult> GetApps()
        {
            var apps = await _appService.GetAppsAsync();
            if (!apps.Value.Any())
            {
                return NotFound();
            }
            return Ok(apps.Value.Select(a => new CreatedApp(a.Id, a.AppToken)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppResponseDTO>> GetApp(int id)
        {
            var app = await _appService.GetAppAsync(id);

            if (app.Value == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AppResponseDTO>(app.Value));
        }

        [HttpPost]
        public async Task<ActionResult<AppResponseDTO>> AddApp(AppDTO appDTO)
        {
            var app = _mapper.Map<App>(appDTO);
            var result = await _appService.AddAppAsync(app);

            return CreatedAtAction(nameof(GetApp), new { id = app.Id }, new CreatedApp(app.Id, app.AppToken));
        }
    }
}
