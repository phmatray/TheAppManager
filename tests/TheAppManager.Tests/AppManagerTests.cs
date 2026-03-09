using TheAppManager.Startup;

namespace TheAppManager.Tests;

public class AppManagerTests
{
    [Fact]
    public void Start_ThrowsOnNullArgs()
    {
        Should.Throw<ArgumentNullException>(() => AppManager.Start(null!));
    }

    [Fact]
    public async Task StartAsync_ThrowsOnNullArgs()
    {
        await Should.ThrowAsync<ArgumentNullException>(() => AppManager.StartAsync(null!));
    }
}
