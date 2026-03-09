using TheAppManager.Modules;

namespace TheAppManager.Sample.Modules;

/// <summary>
/// Adds authentication middleware to the application.
/// </summary>
public class AuthModule : IAppModule
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
    }

    public void ConfigureMiddleware(WebApplication app)
    {
        app.UseAuthentication();
    }
}
