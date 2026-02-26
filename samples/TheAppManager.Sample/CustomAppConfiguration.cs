using TheAppManager.Startup;

namespace TheAppManager.Sample;

/// <summary>
/// Demonstrates extending the sample configuration with authentication middleware.
/// </summary>
public class CustomAppConfiguration : SampleAppConfiguration
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddAuthentication();
    }

    public override void ConfigureMiddleware(WebApplication app)
    {
        base.ConfigureMiddleware(app);
        app.UseAuthentication();
    }

    public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        base.ConfigureEndpoints(endpoints);
        endpoints.MapGet("/custom", () => "Custom Endpoint!");
    }
}
