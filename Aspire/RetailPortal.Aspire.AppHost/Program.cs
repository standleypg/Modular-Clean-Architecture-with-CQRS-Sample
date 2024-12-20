var builder = DistributedApplication.CreateBuilder(args);

// This db created by Aspire isn't the same as the one we run manually using docker-compose.
// Choose only one of the two.
// If you want to use the one created by Aspire, skip the docker-compose command and vice versa.
var postgres = builder.AddPostgres("RetailPortalDb")
    .WithDataVolume()
    .WithPgAdmin();

builder.AddProject<Projects.RetailPortal_Api>("retailportal-api")
    .WithHttpsEndpoint(port: 7005, name: "api")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.AddProject<Projects.RetailPortal_MigrationService>("retailportal-migration-service")
    .WithReference(postgres);

await builder.Build().RunAsync();
