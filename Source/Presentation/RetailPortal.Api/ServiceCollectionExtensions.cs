using RetailPortal.Infrastructure;
using RetailPortal.Infrastructure.Db.Sql;

namespace RetailPortal.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        return services;
    }
}