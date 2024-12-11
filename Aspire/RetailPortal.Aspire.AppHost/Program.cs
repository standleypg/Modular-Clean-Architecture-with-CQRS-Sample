var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("retailportal-db")
    .WithDataVolume()
    .WithPgAdmin();

builder.AddProject<Projects.RetailPortal_Api>("retailportal-api")
    .WithHttpsEndpoint(port: 7005, name: "api")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.AddProject<Projects.RetailPortal_MigrationService>("retailportal-migration-service")
    .WithReference(postgres);

await builder.Build().RunAsync();
