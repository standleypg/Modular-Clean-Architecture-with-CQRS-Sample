using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RetailPortal.Application.Common;
using System.Reflection;

namespace RetailPortal.Application;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR();

        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>), ServiceLifetime.Scoped);
        });

        services.AddTransient<BaseHandler>();

        return services;
    }
}