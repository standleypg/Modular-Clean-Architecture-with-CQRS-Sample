using Microsoft.Extensions.DependencyInjection;

namespace RetailPortal.Application;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}