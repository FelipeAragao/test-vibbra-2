using System.Text;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Glauber.NotificationSystem.Api.Controllers;

public abstract class MainController() : ControllerBase
{
    protected ActionResult FormatBadRequestResponse(IEnumerable<IError> errors)
    {
        var sb = new StringBuilder();
        foreach (var error in errors)
        {
            sb.AppendLine(error.Message);
        }
        
        return BadRequest(new
        {
            error = sb.ToString()
        });
    }

    protected ActionResult FormatNotFoundResponse(IEnumerable<IError> errors)
    {
        var sb = new StringBuilder();
        foreach (var error in errors)
        {
            sb.AppendLine(error.Message);
        }
        
        return NotFound(new
        {
            error = sb.ToString()
        });
    }

    protected ActionResult FormatBadRequestResponse(IEnumerable<IdentityError> errors)
    {
        var sb = new StringBuilder();
        foreach (var error in errors)
        {
            sb.AppendLine(error.Description);
        }
        return BadRequest(new
        {
            error = sb.ToString()
        });
    }
}
