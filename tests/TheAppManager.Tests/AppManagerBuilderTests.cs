using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using TheAppManager.Startup;

namespace TheAppManager.Tests;

public class AppManagerBuilderTests
{
    [Fact]
    public void Build_CallsConfigureServices()
    {
        var strategy = new TestStrategy();
        var builder = new AppManagerBuilder([]);

        builder.Build(strategy);

        Assert.True(strategy.ConfigureServicesCalled);
    }

    [Fact]
    public void Build_CallsConfigureMiddleware()
    {
        var strategy = new TestStrategy();
        var builder = new AppManagerBuilder([]);

        builder.Build(strategy);

        Assert.True(strategy.ConfigureMiddlewareCalled);
    }

    [Fact]
    public void Build_CallsConfigureEndpoints()
    {
        var strategy = new TestStrategy();
        var builder = new AppManagerBuilder([]);

        builder.Build(strategy);

        Assert.True(strategy.ConfigureEndpointsCalled);
    }

    [Fact]
    public void Build_ReturnsAppManagerInstance()
    {
        var strategy = new TestStrategy();
        var builder = new AppManagerBuilder([]);

        var appManager = builder.Build(strategy);

        Assert.NotNull(appManager);
    }

    [Fact]
    public void Build_ThrowsOnNullStrategy()
    {
        var builder = new AppManagerBuilder([]);

        Assert.Throws<ArgumentNullException>(() => builder.Build(null!));
    }

    [Fact]
    public void Constructor_ThrowsOnNullArgs()
    {
        Assert.Throws<ArgumentNullException>(() => new AppManagerBuilder(null!));
    }

    [Fact]
    public void ConfigureBuilder_InvokesCallbackBeforeBuild()
    {
        var strategy = new TestStrategy();
        var builder = new AppManagerBuilder([]);
        var callbackInvoked = false;

        builder.ConfigureBuilder(b =>
        {
            callbackInvoked = true;
            Assert.NotNull(b);
        });
        builder.Build(strategy);

        Assert.True(callbackInvoked);
    }

    [Fact]
    public void ConfigureBuilder_WithNull_DoesNotThrow()
    {
        var strategy = new TestStrategy();
        var builder = new AppManagerBuilder([]);

        builder.ConfigureBuilder(null);
        var appManager = builder.Build(strategy);

        Assert.NotNull(appManager);
    }

    private class TestStrategy : IAppConfigurationStrategy
    {
        public bool ConfigureServicesCalled { get; private set; }
        public bool ConfigureMiddlewareCalled { get; private set; }
        public bool ConfigureEndpointsCalled { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesCalled = true;
        }

        public void ConfigureMiddleware(WebApplication app)
        {
            ConfigureMiddlewareCalled = true;
        }

        public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
        {
            ConfigureEndpointsCalled = true;
        }
    }
}
