using DiWinFormsDemo.Interfaces;

namespace DiWinFormsDemo.Services
{
    public class TaskCounterService : ITaskCounterService
    {
        private int _currentValue;
        private CancellationTokenSource? _cts;
        private Task? _countingTask;
        private readonly SynchronizationContext? _synchronizationContext;

        public event EventHandler<int>? ValueChanged;

        public int CurrentValue => _currentValue;

        public TaskCounterService()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }

        public Task StartCounting(CancellationToken cancellationToken)
        {
            if (_countingTask != null) return Task.CompletedTask;

            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _countingTask = CountAsync(_cts.Token);

            return Task.CompletedTask;
        }

        public void StopCounting()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            _countingTask = null;
        }

        private async Task CountAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, cancellationToken);
                    _currentValue++;

                    // Raise event on UI thread
                    if (_synchronizationContext != null)
                    {
                        _synchronizationContext.Post(_ => ValueChanged?.Invoke(this, _currentValue), null);
                    }
                    else
                    {
                        ValueChanged?.Invoke(this, _currentValue);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Task was canceled, which is expected behavior
            }
        }

        public void Dispose()
        {
            StopCounting();
            _cts?.Dispose();
        }
    }
}
