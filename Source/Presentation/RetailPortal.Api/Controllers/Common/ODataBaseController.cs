using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using RetailPortal.Api.Common;
using RetailPortal.Api.Common.Http;
using RetailPortal.Shared.DTOs.Common;
using System.Globalization;
using System.Web;

namespace RetailPortal.Api.Controllers.Common;

public class ODataBaseController: ODataController
{
    protected ActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
            return this.Problem();

        if (errors.All(err => err.Type == ErrorType.Validation))
            return this.ValidationProblem(errors);

        this.HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        return this.Problem(errors[0]);
    }

    private ObjectResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return this.Problem(statusCode: statusCode, title: error.Description);
    }

    private ActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }
        return this.ValidationProblem(modelStateDictionary);
    }
}