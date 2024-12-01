using ErrorOr;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using RetailPortal.Shared.DTOs.Common;
using System.Globalization;
using System.Web;

namespace RetailPortal.Application.Common;

public static class IQueryableExtensions
{
    private const int DefaultPageSize = 5;

    public static async Task<ErrorOr<ODataResponse<T>>> GetODataResponseAsync<T>(this IQueryable<T> queryable,  ODataQueryOptions<T>? options)
    {
        // Apply $filter (but not $skip, $top yet)
        var filteredData = ApplyODataQuery(queryable, options);

        // Get total count based on filtered data (before applying pagination)
        int? count = null;
        if (options.Count?.Value == true)
        {
            count = filteredData.Count();
        }

        // Apply $skip and $top after filtering
        var queriedData = options.ApplyTo(filteredData) as IQueryable<T>;
        var result = await queriedData?.ToListAsync();

        // Determine next page URL if $top and $skip are provided
        string? nextPage = null;
        if (result != null)
        {
            nextPage = GetNextPageUri(options, filteredData);
        }

        return new ODataResponse<T>
        {
            Value = result,
            Count = count,
            NextPage = nextPage
        };
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