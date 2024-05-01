namespace TheAppManager.Startup;

public class AppManagerBuilder(string[] args)
{
    private readonly WebApplicationBuilder _builder = WebApplication.CreateBuilder(args);
    private WebApplication? _app;

    public AppManager Build(IAppConfigurationStrategy configurationStrategy)
    {
        configurationStrategy.ConfigureServices(_builder.Services);
        _app = _builder.Build();
        configurationStrategy.ConfigureMiddleware(_app);
        configurationStrategy.ConfigureEndpoints(_app);
        return new AppManager(_app);
    }
}