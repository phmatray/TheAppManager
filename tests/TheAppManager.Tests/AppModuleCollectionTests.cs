using TheAppManager.Modules;

namespace TheAppManager.Tests;

public class AppModuleCollectionTests
{
    [Fact]
    public void Add_Generic_AddsModule()
    {
        var collection = new AppModuleCollection();

        collection.Add<EmptyModule>();

        collection.Count().ShouldBe(1);
    }

    [Fact]
    public void Add_Instance_AddsModule()
    {
        var collection = new AppModuleCollection();
        var module = new EmptyModule();

        collection.Add(module);

        collection.Count().ShouldBe(1);
        collection.First().ShouldBeSameAs(module);
    }

    [Fact]
    public void Add_Instance_ThrowsOnNull()
    {
        var collection = new AppModuleCollection();

        Should.Throw<ArgumentNullException>(() => collection.Add(null!));
    }

    [Fact]
    public void AddIf_True_AddsModule()
    {
        var collection = new AppModuleCollection();

        collection.AddIf<EmptyModule>(true);

        collection.Count().ShouldBe(1);
    }

    [Fact]
    public void AddIf_False_DoesNotAdd()
    {
        var collection = new AppModuleCollection();

        collection.AddIf<EmptyModule>(false);

        collection.ShouldBeEmpty();
    }

    [Fact]
    public void GetModules_PreservesRegistrationOrder()
    {
        var first = new EmptyModule();
        var second = new EmptyModule();
        var third = new EmptyModule();
        var collection = new AppModuleCollection();
        collection.Add(first);
        collection.Add(second);
        collection.Add(third);

        var modules = collection.GetModules();

        modules[0].ShouldBeSameAs(first);
        modules[1].ShouldBeSameAs(second);
        modules[2].ShouldBeSameAs(third);
    }

    [Fact]
    public void FluentChaining_Works()
    {
        var collection = new AppModuleCollection()
            .Add<EmptyModule>()
            .AddIf<EmptyModule>(true)
            .Add(new EmptyModule());

        collection.Count().ShouldBe(3);
    }

    private class EmptyModule : IAppModule;
}
