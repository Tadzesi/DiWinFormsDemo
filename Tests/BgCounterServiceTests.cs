using DiWinFormsDemo.Services;

namespace CounterApp.Tests;

public class BgCounterServiceTests : IDisposable
{
    private readonly BgWorkerCounterService _counterService;

    public BgCounterServiceTests()
    {
        _counterService = new BgWorkerCounterService();
        //_cts = new CancellationTokenSource();
    }

    public void Dispose()
    {
        //_cts.Dispose();
        //_counterService.Dispose();
    }

    [Fact]
    public void InitialValue_ShouldBeZero()
    {
        // Assert
        Assert.Equal(0, _counterService.CurrentValue);
    }

    [Fact]
    public async Task StartCounting_ShouldIncrementValue()
    {
        // Arrange
        var eventRaised = false;
        var initialValue = _counterService.CurrentValue;
        _counterService.ValueChanged += (s, value) => eventRaised = true;

        // Act
        _counterService.StartCounting();
        await Task.Delay(1500); // Wait for more than 1 second

        // Assert
        Assert.True(_counterService.CurrentValue > initialValue);
        Assert.True(eventRaised);
    }

    [Fact]
    public async Task StopCounting_ShouldStopIncrementing()
    {
        // Arrange
        _counterService.StartCounting();
        await Task.Delay(1500); // Wait for more than 1 second
        var valueBeforeStop = _counterService.CurrentValue;

        // Act
        _counterService.StopCounting();
        await Task.Delay(1500); // Wait again

        // Assert
        Assert.Equal(valueBeforeStop + 1, _counterService.CurrentValue);
    }

    [Fact]
    public async Task ValueChanged_ShouldRaiseWithCorrectValue()
    {
        // Arrange
        var receivedValue = -1;
        _counterService.ValueChanged += (s, value) => receivedValue = value;

        // Act
        _counterService.StartCounting();
        await Task.Delay(1500); // Wait for more than 1 second

        // Assert
        Assert.Equal(_counterService.CurrentValue, receivedValue);
    }
}