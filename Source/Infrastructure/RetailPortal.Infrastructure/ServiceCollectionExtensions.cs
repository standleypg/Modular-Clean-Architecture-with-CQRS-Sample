using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using RetailPortal.Domain.Interfaces.Infrastructure.Auth;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.Repositories;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;
using RetailPortal.Domain.Interfaces.Infrastructure.Services;
using RetailPortal.Infrastructure.Auth;
using RetailPortal.Infrastructure.Data.Context;
using RetailPortal.Infrastructure.Data.Repositories;
using RetailPortal.Infrastructure.Data.UnitOfWork;
using RetailPortal.Infrastructure.Services;
using RetailPortal.Shared.Constants;
using System.Text;

namespace RetailPortal.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);

        services
            .AddConfigurationBinding(configuration)
            .AddAuth(configuration);

        services
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

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<Appsettings.JwtSettings>>().Value;
        var googleOptions = services.BuildServiceProvider().GetRequiredService<IOptions<Appsettings.GoogleSettings>>().Value;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                };
            })
            .AddJwtBearer(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.Authority = googleOptions.Authority;
                options.Audience = googleOptions.ClientId;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = googleOptions.Authority,
                    ValidateAudience = true,
                    ValidAudience = googleOptions.ClientId,
                    ValidateLifetime = true,
                };
            })
            .AddMicrosoftIdentityWebApi(configuration.GetSection(Appsettings.AzureAdSettings.SectionName), Appsettings.AzureAdSettings.JwtBearerScheme);

        services.AddAuthorization();

        return services;
    }

    private static IServiceCollection AddConfigurationBinding(this IServiceCollection services, IConfiguration configuration)
    {
        BindAndAdd<Appsettings.JwtSettings>(Appsettings.JwtSettings.SectionName);
        BindAndAdd<Appsettings.GoogleSettings>(Appsettings.GoogleSettings.SectionName);
        BindAndAdd<Appsettings.AzureAdSettings>(Appsettings.AzureAdSettings.SectionName);

        return services;

        void BindAndAdd<TSettings>(string sectionName) where TSettings : class, new()
        {
            var settings = new TSettings();
            configuration.Bind(sectionName, settings);
            services.AddSingleton(Options.Create(settings));
        }
    }
}