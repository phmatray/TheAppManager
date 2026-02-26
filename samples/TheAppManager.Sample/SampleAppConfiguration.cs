using TheAppManager.Sample.Forecast;
using TheAppManager.Startup;

namespace TheAppManager.Sample;

/// <summary>
/// Sample configuration that extends DefaultAppConfiguration with Swagger,
/// weather forecast services, and endpoints.
/// </summary>
public class SampleAppConfiguration : DefaultAppConfiguration
{
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<WeatherForecastService>();
    }

    public override void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        base.ConfigureMiddleware(app);
    }

    public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        base.ConfigureEndpoints(endpoints);
        endpoints.MapGetWeatherForecasts();
    }
}
