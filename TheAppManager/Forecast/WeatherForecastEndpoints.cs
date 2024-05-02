namespace TheAppManager.Forecast;

public static class WeatherForecastEndpoints
{
    public static void MapGetWeatherForecasts(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast", Handler)
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
    
    private static IResult Handler(WeatherForecastService service)
    {
        try
        {
            return Results.Ok(service.GetForecasts());
        }
        catch (Exception e)
        {
            return Results.Problem(e.StackTrace, e.Message, StatusCodes.Status500InternalServerError);
        }
    }
}