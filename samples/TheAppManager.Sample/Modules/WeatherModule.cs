using TheAppManager.Modules;
using TheAppManager.Sample.Forecast;

namespace TheAppManager.Sample.Modules;

/// <summary>
/// Registers weather forecast services and endpoints.
/// </summary>
public class WeatherModule : IAppModule
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<WeatherForecastService>();
    }

    public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGetWeatherForecasts();
    }
}
