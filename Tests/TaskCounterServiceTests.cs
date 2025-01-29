using DiWinFormsDemo.Services;

namespace CounterApp.Tests;

public class TaskCounterServiceTests : IDisposable
{
    private readonly TaskCounterService _counterService;
    private readonly CancellationTokenSource _cts;

    public TaskCounterServiceTests()
    {
        _counterService = new TaskCounterService();
        _cts = new CancellationTokenSource();
    }

    public void Dispose()
    {
        _cts.Dispose();
        _counterService.Dispose();
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
        await _counterService.StartCounting(_cts.Token);
        await Task.Delay(1500); // Wait for more than 1 second

        // Assert
        Assert.True(_counterService.CurrentValue > initialValue);
        Assert.True(eventRaised);
    }

    [Fact]
    public async Task StopCounting_ShouldStopIncrementing()
    {
        // Arrange
        await _counterService.StartCounting(_cts.Token);
        await Task.Delay(1500); // Wait for more than 1 second
        var valueBeforeStop = _counterService.CurrentValue;

        // Act
        _counterService.StopCounting();
        await Task.Delay(1500); // Wait again

        // Assert
        Assert.Equal(valueBeforeStop, _counterService.CurrentValue);
    }

    [Fact]
    public async Task ValueChanged_ShouldRaiseWithCorrectValue()
    {
        // Arrange
        var receivedValue = -1;
        _counterService.ValueChanged += (s, value) => receivedValue = value;

        // Act
        await _counterService.StartCounting(_cts.Token);
        await Task.Delay(1500); // Wait for more than 1 second

        // Assert
        Assert.Equal(_counterService.CurrentValue, receivedValue);
    }

    [Fact]
    public async Task Cancellation_ShouldStopCounting()
    {
        // Arrange
        await _counterService.StartCounting(_cts.Token);
        await Task.Delay(1500);
        var valueBeforeCancellation = _counterService.CurrentValue;

        // Act
        _cts.Cancel();
        await Task.Delay(1500);

        // Assert
        Assert.Equal(valueBeforeCancellation, _counterService.CurrentValue);
    }
}