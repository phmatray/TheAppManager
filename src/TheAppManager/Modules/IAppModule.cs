using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace TheAppManager.Modules;

/// <summary>
/// Defines a composable module for configuring an ASP.NET Core web application.
/// Implement this interface to encapsulate related services, middleware, and endpoints.
/// All methods have default no-op implementations — only override what you need.
/// Modules are applied in the order they are registered.
/// </summary>
public interface IAppModule
{
    /// <summary>
    /// Configures services in the dependency injection container.
    /// </summary>
    /// <param name="builder">The web application builder, providing access to services, configuration, and environment.</param>
    void ConfigureServices(WebApplicationBuilder builder)
    {
    }

    /// <summary>
    /// Configures the middleware pipeline.
    /// </summary>
    /// <param name="app">The web application to configure middleware on.</param>
    void ConfigureMiddleware(WebApplication app)
    {
    }

    /// <summary>
    /// Configures the endpoint routes.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to configure.</param>
    void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
    }
}
