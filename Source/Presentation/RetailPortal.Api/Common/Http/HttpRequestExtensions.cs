using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Collections.Concurrent;

namespace RetailPortal.Api.Common.Http;

internal static class HttpRequestExtensions
{
    public static ODataQueryOptions<T> GetODataQueryOptions<T>(this HttpRequest request)
    {
        var path = request.ODataFeature().Path;
        var edmModel = EdmModelCache.Get(typeof(T), request.HttpContext.RequestServices);

        var queryContext = new ODataQueryContext(
            edmModel,
            typeof(T),
            path
        );

        // Create and validate the query options.
        return new ODataQueryOptions<T>(queryContext, request);
    }
}


internal static class EdmModelCache
{
    private static readonly ConcurrentDictionary<string, IEdmModel> _modelCache = new();

    public static IEdmModel Get(Type entityType, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(entityType);

        return _modelCache.GetOrAdd(entityType.FullName!, key =>
        {
            var assemblyResolver = serviceProvider.GetService<IAssemblyResolver>();
            var builder = new ODataConventionModelBuilder(assemblyResolver, isQueryCompositionMode: true);
            EntityTypeConfiguration entityTypeConfiguration = builder.AddEntityType(entityType);
            builder.AddEntitySet(entityType.Name, entityTypeConfiguration);
            var model = builder.GetEdmModel();
            return model;
        });
    }

}