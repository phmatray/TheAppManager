namespace TheAppManager.Startup;

/// <summary>
/// Defines a strategy for configuring an ASP.NET Core web application.
/// Implement this interface to provide custom service registration, middleware, and endpoint configuration.
/// </summary>
public interface IAppConfigurationStrategy
{
    /// <summary>
    /// Configures services in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    void ConfigureServices(IServiceCollection services);

    /// <summary>
    /// Configures the middleware pipeline.
    /// </summary>
    /// <param name="app">The web application to configure middleware on.</param>
    void ConfigureMiddleware(WebApplication app);

    /// <summary>
    /// Configures the endpoint routes.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to configure.</param>
    void ConfigureEndpoints(IEndpointRouteBuilder endpoints);
}
