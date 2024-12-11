using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Logging;
using RetailPortal.Api;
using RetailPortal.Application;
using RetailPortal.Aspire.ServiceDefaults;
using RetailPortal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var configuration = builder.Configuration;

builder.Services
    .AddApi(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(configuration);

builder.Services.AddControllers().AddOData(options =>
    options.Filter().Select().Expand().OrderBy().Count().SetMaxTop(1000)
);

var app = builder.Build();

app.MapDefaultEndpoints();

app.AddApi();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();