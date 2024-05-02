using TheAppManager.Forecast;

namespace TheAppManager.Startup;

public class DefaultAppConfiguration : IAppConfigurationStrategy
{
    public virtual void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddScoped<WeatherForecastService>();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public virtual void ConfigureMiddleware(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
    }

    public virtual void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Example endpoint
        endpoints.MapGet("/", () => "Hello World!");
        
        // Custom method to add specific endpoints
        endpoints.MapGetWeatherForecasts();
    }
}