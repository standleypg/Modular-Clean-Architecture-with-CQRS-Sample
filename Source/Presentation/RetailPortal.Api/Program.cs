using RetailPortal.Api;
using RetailPortal.Application;
using RetailPortal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure(configuration);

var app = builder.Build();

app.AddApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();