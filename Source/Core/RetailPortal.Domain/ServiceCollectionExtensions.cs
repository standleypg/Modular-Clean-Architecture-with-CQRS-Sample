using Microsoft.Extensions.DependencyInjection;

namespace RetailPortal.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}