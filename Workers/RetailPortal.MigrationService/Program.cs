using RetailPortal.Aspire.ServiceDefaults;
using RetailPortal.Infrastructure.Data.Context;
using RetailPortal.MigrationService;
using RetailPortal.Shared.Constants;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Appsettings.PostgresSQLConnectionName));

builder.AddNpgsqlDbContext<ApplicationDbContext>(Appsettings.PostgresSQLConnectionName);

var host = builder.Build();

await host.RunAsync();
