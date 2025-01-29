namespace DiWinFormsDemo.Interfaces
{
    public interface ITaskCounterService
    {
        int CurrentValue { get; }
        event EventHandler<int> ValueChanged;
        Task StartCounting(CancellationToken cancellationToken);
        void StopCounting();
    }
}
