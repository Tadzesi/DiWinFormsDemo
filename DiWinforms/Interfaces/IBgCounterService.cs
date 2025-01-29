namespace DiWinFormsDemo.Interfaces
{
    public interface IBgCounterService
    {
        int CurrentValue { get; }
        event EventHandler<int> ValueChanged;
        void StartCounting();
        void StopCounting();


    }
}
