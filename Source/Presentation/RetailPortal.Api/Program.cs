using Microsoft.AspNetCore.OData;
using RetailPortal.Api;
using RetailPortal.Application;
using RetailPortal.Aspire.ServiceDefaults;
using RetailPortal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var configuration = builder.Configuration;

builder.Services.AddControllers().AddOData(options =>
    options.Filter().Select().Expand().OrderBy().Count().SetMaxTop(1000)
);

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure(configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.AddApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();