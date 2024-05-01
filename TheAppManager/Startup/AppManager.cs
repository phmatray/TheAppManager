namespace TheAppManager.Startup;

public class AppManager
{
    private readonly WebApplication _app;

    internal AppManager(WebApplication app)
    {
        _app = app;
    }

    public void Run()
    {
        _app.Run();
    }

    // Static method to simplify startup configuration and execution
    public static void StartApplication(string[] args, IAppConfigurationStrategy? configurationStrategy = null)
    {
        configurationStrategy ??= new DefaultAppConfiguration();
        var appManager = new AppManagerBuilder(args).Build(configurationStrategy);
        appManager.Run();
    }
}