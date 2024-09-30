using System.Text;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Glauber.NotificationSystem.Api.Controllers;

public abstract class MainController() : ControllerBase
{
    protected ActionResult FormatResponse(Result result)
    {
        if (result.IsFailed)
        {
            var sb = new StringBuilder();
            result.Errors.ForEach(error => sb.AppendLine(error.Message));
            
            return BadRequest(new
            {
                error = sb.ToString()
            });
        }

        return Ok(result);
    }

    protected ActionResult FormatErrorResponse(IEnumerable<IdentityError> errors)
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
