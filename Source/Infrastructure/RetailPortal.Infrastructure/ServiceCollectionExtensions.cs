using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RetailPortal.Domain.Interfaces.Infrastructure.Auth;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.Repositories;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;
using RetailPortal.Domain.Interfaces.Infrastructure.Services;
using RetailPortal.Infrastructure.Auth;
using RetailPortal.Infrastructure.Data.Context;
using RetailPortal.Infrastructure.Data.Repositories;
using RetailPortal.Infrastructure.Data.UnitOfWork;
using RetailPortal.Infrastructure.Services;

namespace RetailPortal.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddDbContext(configuration);

        services
            .AddSingleton(Options.Create(jwtSettings))
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddSingleton<IPasswordHasher, PasswordHasher>()
            .AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("retailportal-db"));
        });

        return services;
    }
}