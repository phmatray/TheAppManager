namespace TheAppManager.Startup;

/// <summary>
/// Manages the lifecycle of an ASP.NET Core web application.
/// Use <see cref="StartApplication"/> or <see cref="StartApplicationAsync"/> for a simplified startup experience,
/// or use <see cref="AppManagerBuilder"/> for more control.
/// </summary>
public class AppManager
{
    private readonly WebApplication _app;

    internal AppManager(WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);
        _app = app;
    }

    /// <summary>
    /// Runs the web application synchronously.
    /// </summary>
    public void Run()
    {
        _app.Run();
    }

    /// <summary>
    /// Runs the web application asynchronously.
    /// </summary>
    /// <returns>A task that represents the lifetime of the application.</returns>
    public Task RunAsync()
    {
        return _app.RunAsync();
    }

    /// <summary>
    /// Creates, configures, and runs a web application using the provided strategy.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    /// <param name="configurationStrategy">
    /// The configuration strategy to use. If <c>null</c>, <see cref="DefaultAppConfiguration"/> is used.
    /// </param>
    /// <param name="configureBuilder">
    /// An optional callback to configure the <see cref="WebApplicationBuilder"/> before building.
    /// </param>
    public static void StartApplication(
        string[] args,
        IAppConfigurationStrategy? configurationStrategy = null,
        Action<WebApplicationBuilder>? configureBuilder = null)
    {
        ArgumentNullException.ThrowIfNull(args);

        configurationStrategy ??= new DefaultAppConfiguration();
        var appManager = new AppManagerBuilder(args)
            .ConfigureBuilder(configureBuilder)
            .Build(configurationStrategy);
        appManager.Run();
    }

    /// <summary>
    /// Creates, configures, and runs a web application asynchronously using the provided strategy.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    /// <param name="configurationStrategy">
    /// The configuration strategy to use. If <c>null</c>, <see cref="DefaultAppConfiguration"/> is used.
    /// </param>
    /// <param name="configureBuilder">
    /// An optional callback to configure the <see cref="WebApplicationBuilder"/> before building.
    /// </param>
    /// <returns>A task that represents the lifetime of the application.</returns>
    public static Task StartApplicationAsync(
        string[] args,
        IAppConfigurationStrategy? configurationStrategy = null,
        Action<WebApplicationBuilder>? configureBuilder = null)
    {
        ArgumentNullException.ThrowIfNull(args);

        configurationStrategy ??= new DefaultAppConfiguration();
        var appManager = new AppManagerBuilder(args)
            .ConfigureBuilder(configureBuilder)
            .Build(configurationStrategy);
        return appManager.RunAsync();
    }
}
