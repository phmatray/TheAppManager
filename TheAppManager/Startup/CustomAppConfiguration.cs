namespace TheAppManager.Startup;

public class CustomAppConfiguration : DefaultAppConfiguration
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // Retain base configurations
        base.ConfigureServices(services);
        
        // Additional custom service
        services.AddAuthentication();
    }

    public override void ConfigureMiddleware(WebApplication app)
    {
        // Retain base middleware
        base.ConfigureMiddleware(app);
        
        // Apply authentication middleware
        app.UseAuthentication();
    }

    public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Retain base endpoints
        base.ConfigureEndpoints(endpoints);
        
        // Additional endpoint
        endpoints.MapGet("/custom", () => "Custom Endpoint!");
    }
}