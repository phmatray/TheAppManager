namespace TheAppManager.Startup;

/// <summary>
/// A minimal default configuration strategy that sets up HTTPS redirection
/// and a basic root endpoint. Intended as a starting point or fallback configuration.
/// </summary>
public class DefaultAppConfiguration : IAppConfigurationStrategy
{
    /// <inheritdoc />
    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    /// <inheritdoc />
    public virtual void ConfigureMiddleware(WebApplication app)
    {
        app.UseHttpsRedirection();
    }

    /// <inheritdoc />
    public virtual void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", () => "Hello World!");
    }
}
