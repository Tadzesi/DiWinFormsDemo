using DiWinFormsDemo.Interfaces;

namespace DiWinFormsDemo.Services
{
    public class BgWorkerCounterService : IBgCounterService
    {
        private readonly System.ComponentModel.BackgroundWorker _worker;
        private int _currentValue;
        private bool _isRunning;

        public event EventHandler<int> ValueChanged;

        public int CurrentValue => _currentValue;

        public BgWorkerCounterService()
        {
            _worker = new System.ComponentModel.BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += Worker_DoWork;
        }

        private async void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!_worker.CancellationPending)
            {
                await Task.Delay(1000);
                _currentValue++;
                ValueChanged?.Invoke(this, _currentValue);
            }
        }

        public void StartCounting()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _worker.RunWorkerAsync();
            }
        }

        public void StopCounting()
        {
            if (_isRunning)
            {
                _worker.CancelAsync();
                _isRunning = false;
            }
        }
    }
}
