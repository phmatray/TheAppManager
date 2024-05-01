namespace TheAppManager.Startup;

public interface IAppConfigurationStrategy
{
    void ConfigureServices(IServiceCollection services);
    void ConfigureMiddleware(WebApplication app);
    void ConfigureEndpoints(IEndpointRouteBuilder endpoints);
}