using TheAppManager.Startup;

namespace TheAppManager.Tests;

public class AppManagerTests
{
    [Fact]
    public void StartApplication_ThrowsOnNullArgs()
    {
        Assert.Throws<ArgumentNullException>(() => AppManager.StartApplication(null!));
    }

    [Fact]
    public async Task StartApplicationAsync_ThrowsOnNullArgs()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => AppManager.StartApplicationAsync(null!));
    }
}
