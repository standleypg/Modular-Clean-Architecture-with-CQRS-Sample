using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RetailPortal.Application.Common;
using RetailPortal.Application.Services.Role;
using RetailPortal.Domain.Interfaces.Application.Services;
using System.Reflection;

namespace RetailPortal.Application;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR();

        services
            .AddScoped<IRoleService, RoleService>();

        return services;
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>), ServiceLifetime.Scoped);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), ServiceLifetime.Scoped);
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<BaseHandler>();

        return services;
    }
}