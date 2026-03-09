using TheAppManager.Modules;

namespace TheAppManager.Tests;

public class ReplaceModuleTests
{
    private class ModuleA : IAppModule;
    private class ModuleB : IAppModule;
    private class ModuleC : IAppModule;
    private class ReplacementModule : IAppModule;

    [Fact]
    public void Replace_SwapsModuleAtSamePosition()
    {
        // Arrange
        var collection = new AppModuleCollection();
        collection.Add<ModuleA>().Add<ModuleB>().Add<ModuleC>();

        // Act
        collection.Replace<ModuleB, ReplacementModule>();

        // Assert
        var modules = collection.GetModules();
        modules.Count.ShouldBe(3);
        modules[0].ShouldBeOfType<ModuleA>();
        modules[1].ShouldBeOfType<ReplacementModule>();
        modules[2].ShouldBeOfType<ModuleC>();
    }

    [Fact]
    public void Replace_ThrowsWhenModuleNotFound()
    {
        // Arrange
        var collection = new AppModuleCollection();
        collection.Add<ModuleA>();

        // Act & Assert
        Should.Throw<InvalidOperationException>(
            () => collection.Replace<ModuleB, ReplacementModule>());
    }

    [Fact]
    public void Replace_ReturnsSelfForChaining()
    {
        // Arrange
        var collection = new AppModuleCollection();
        collection.Add<ModuleA>();

        // Act
        var result = collection.Replace<ModuleA, ReplacementModule>();

        // Assert
        result.ShouldBeSameAs(collection);
    }

    [Fact]
    public void Replace_OnlyReplacesFirstOccurrence()
    {
        // Arrange
        var collection = new AppModuleCollection();
        collection.Add<ModuleA>().Add<ModuleA>().Add<ModuleC>();

        // Act
        collection.Replace<ModuleA, ReplacementModule>();

        // Assert
        var modules = collection.GetModules();
        modules.Count.ShouldBe(3);
        modules[0].ShouldBeOfType<ReplacementModule>();
        modules[1].ShouldBeOfType<ModuleA>();
        modules[2].ShouldBeOfType<ModuleC>();
    }
}
