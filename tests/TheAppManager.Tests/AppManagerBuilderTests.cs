using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using TheAppManager.Modules;
using TheAppManager.Startup;

namespace TheAppManager.Tests;

public class AppManagerBuilderTests
{
    [Fact]
    public void Build_CallsConfigureServices()
    {
        var module = new TestModule();
        var modules = new AppModuleCollection();
        modules.Add(module);

        new AppManagerBuilder([]).Build(modules);

        module.ConfigureServicesCalled.ShouldBeTrue();
    }

    [Fact]
    public void Build_CallsConfigureMiddleware()
    {
        var module = new TestModule();
        var modules = new AppModuleCollection();
        modules.Add(module);

        new AppManagerBuilder([]).Build(modules);

        module.ConfigureMiddlewareCalled.ShouldBeTrue();
    }

    [Fact]
    public void Build_CallsConfigureEndpoints()
    {
        var module = new TestModule();
        var modules = new AppModuleCollection();
        modules.Add(module);

        new AppManagerBuilder([]).Build(modules);

        module.ConfigureEndpointsCalled.ShouldBeTrue();
    }

    [Fact]
    public void Build_ReturnsAppManagerInstance()
    {
        var modules = new AppModuleCollection();

        var appManager = new AppManagerBuilder([]).Build(modules);

        appManager.ShouldNotBeNull();
    }

    [Fact]
    public void Build_ThrowsOnNullModules()
    {
        var builder = new AppManagerBuilder([]);

        Should.Throw<ArgumentNullException>(() => builder.Build(null!));
    }

    [Fact]
    public void Constructor_ThrowsOnNullArgs()
    {
        Should.Throw<ArgumentNullException>(() => new AppManagerBuilder(null!));
    }

    [Fact]
    public void ConfigureBuilder_InvokesCallbackBeforeBuild()
    {
        var modules = new AppModuleCollection();
        var callbackInvoked = false;

        var builder = new AppManagerBuilder([]);
        builder.ConfigureBuilder(b =>
        {
            callbackInvoked = true;
            b.ShouldNotBeNull();
        });
        builder.Build(modules);

        callbackInvoked.ShouldBeTrue();
    }

    [Fact]
    public void ConfigureBuilder_WithNull_DoesNotThrow()
    {
        var modules = new AppModuleCollection();
        var builder = new AppManagerBuilder([]);

        builder.ConfigureBuilder(null);
        var appManager = builder.Build(modules);

        appManager.ShouldNotBeNull();
    }

    [Fact]
    public void Build_AppliesModulesInRegistrationOrder()
    {
        var callOrder = new List<string>();
        var first = new TrackingModule("first", callOrder);
        var second = new TrackingModule("second", callOrder);
        var third = new TrackingModule("third", callOrder);

        var modules = new AppModuleCollection();
        modules.Add(first);
        modules.Add(second);
        modules.Add(third);

        new AppManagerBuilder([]).Build(modules);

        callOrder.ShouldBe(["first", "second", "third"]);
    }

    [Fact]
    public void Build_WithEmptyModules_Succeeds()
    {
        var modules = new AppModuleCollection();

        var appManager = new AppManagerBuilder([]).Build(modules);

        appManager.ShouldNotBeNull();
    }

    private class TestModule : IAppModule
    {
        public bool ConfigureServicesCalled { get; private set; }
        public bool ConfigureMiddlewareCalled { get; private set; }
        public bool ConfigureEndpointsCalled { get; private set; }

        public void ConfigureServices(WebApplicationBuilder builder)
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

    private class TrackingModule(string name, List<string> callOrder) : IAppModule
    {
        public void ConfigureServices(WebApplicationBuilder builder)
        {
            callOrder.Add(name);
        }
    }
}
