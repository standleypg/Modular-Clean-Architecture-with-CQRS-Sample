using RetailPortal.Infrastructure;
using RetailPortal.Infrastructure.Db.Sql;

namespace RetailPortal.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        SqlHelper.GetSqlFromFile("Tables", "Role", "v0");
        services.AddInfrastructure();
        return services;
    }
}