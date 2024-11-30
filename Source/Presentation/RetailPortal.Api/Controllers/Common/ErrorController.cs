using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RetailPortal.Api.Controllers.Common;

public class ErrorController: ControllerBase
{
    [Route("/error")]
    protected ActionResult Error()
    {
        Exception? exception = this.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return exception switch
        {
            ValidationException validationException => this.BadRequest(validationException.Errors),
            _ => this.StatusCode(500, "Internal Server Error")
        };
    }
}