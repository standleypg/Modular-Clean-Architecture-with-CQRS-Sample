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

     private const int DefaultPageSize = 5;

    protected ActionResult<ODataResponse<T>> GetODataResponse<T>(IQueryable<T> data)
    {
        var options =
            Request.GetODataQueryOptions<T>();

        // Apply $filter (but not $skip, $top yet)
        var filteredData = ApplyODataQuery(data, options);

        // Get total count based on filtered data (before applying pagination)
        int? count = null;
        if (options.Count?.Value == true)
        {
            count = filteredData.Count();
        }

        // Apply $skip and $top after filtering
        var queriedData = options.ApplyTo(filteredData) as IQueryable<T>;
        var result = queriedData?.ToList();

        // Determine next page URL if $top and $skip are provided
        string? nextPage = null;
        if (result != null)
        {
            nextPage = GetNextPageUri(options, filteredData);
        }

        return Ok(new ODataResponse<T>
        {
            Value = result,
            Count = count,
            NextPage = nextPage
        });
    }

    private static IQueryable<T> ApplyODataQuery<T>(IQueryable<T> data, ODataQueryOptions<T> options)
    {
        if (options.Filter != null)
        {
            data = options.Filter.ApplyTo(data, new ODataQuerySettings()) as IQueryable<T> ?? data;
        }

        if (options.OrderBy != null)
        {
            data = options.OrderBy.ApplyTo(data, new ODataQuerySettings());
        }

        return data;
    }

    private static string? GetNextPageUri<T>(ODataQueryOptions<T> options, IQueryable<T> data)
    {
        var skip = options.Skip?.Value ?? 0;
        var top = options.Top?.Value ?? DefaultPageSize;

        if (!data.Skip(skip + top).Any()) return null;

        var query = HttpUtility.ParseQueryString(options.Request.QueryString.ToString());
        query.Set("$skip", (skip + top).ToString());
        query.Set("$top", top.ToString());

        var rawNextPage = $"{options.Request.Scheme}://{options.Request.Host}{options.Request.Path}?{query}";
        return new Uri(HttpUtility.UrlDecode(rawNextPage)).AbsoluteUri.ToString(CultureInfo.InvariantCulture);
    }
}