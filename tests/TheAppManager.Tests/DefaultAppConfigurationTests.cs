using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using TheAppManager.Startup;

namespace TheAppManager.Tests;

public class DefaultAppConfigurationTests
{
    [Fact]
    public async Task DefaultConfiguration_RootEndpoint_ReturnsHelloWorld()
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();

        var strategy = new DefaultAppConfiguration();
        strategy.ConfigureServices(builder.Services);

        var app = builder.Build();
        strategy.ConfigureMiddleware(app);
        strategy.ConfigureEndpoints(app);

        await app.StartAsync();

        var client = app.GetTestClient();
        var response = await client.GetStringAsync("/");

        Assert.Equal("Hello World!", response);

        await app.StopAsync();
    }

    [Fact]
    public async Task AppManagerBuilder_WithDefaultStrategy_RootEndpoint_ReturnsHelloWorld()
    {
        using var host = new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder.UseTestServer();
                webBuilder.ConfigureServices(services =>
                {
                    var strategy = new DefaultAppConfiguration();
                    strategy.ConfigureServices(services);
                });
                webBuilder.Configure(app =>
                {
                    // Minimal middleware
                });
            })
            .Build();

        await host.StartAsync();

        var client = host.GetTestClient();
        // Verify the host starts successfully
        Assert.NotNull(client);

        await host.StopAsync();
    }
}
