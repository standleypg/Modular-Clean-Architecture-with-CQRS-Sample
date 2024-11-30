namespace RetailPortal.Api;

public static class WebApplicationExtensions
{
    public static  WebApplication AddApi(this  WebApplication app)
    {
        app.AddDevelopment();
        return app;
    }

    private static WebApplication AddDevelopment(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v0.json", "API v0.0");
            });
        }
        return app;
    }
}