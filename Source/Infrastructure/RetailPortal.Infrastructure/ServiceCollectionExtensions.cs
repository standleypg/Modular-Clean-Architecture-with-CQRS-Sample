using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetailPortal.Core.Interfaces.UnitOfWork;
using RetailPortal.Infrastructure.Data.Context;
using RetailPortal.Infrastructure.Data.Repositories;
using RetailPortal.Infrastructure.Data.UnitOfWork;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(Appsettings.ConnectionString));
        });

        return services;
    }
}