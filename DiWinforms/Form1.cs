using DiWinFormsDemo.Custom;
using DiWinFormsDemo.Interfaces;

namespace DiWinFormsDemo
{
    public partial class Form1 : Form
    {
        private readonly IBgCounterService _counterService;
        private readonly BgCounterDisplay _counterDisplay;
        private readonly ITaskCounterService _taskCounterService;
        private readonly TaskCounterDisplay _taskCounterDisplay;
        private CancellationTokenSource _cancelationToken;

        public Form1(
            IBgCounterService counterService,
            BgCounterDisplay counterDisplay,
            ITaskCounterService taskCounterService,
            TaskCounterDisplay taskCounterDisplay)
        {
            InitializeComponent();

            _counterService = counterService;
            _counterDisplay = counterDisplay;
            _taskCounterService = taskCounterService;
            _taskCounterDisplay = taskCounterDisplay;
            _cancelationToken = new CancellationTokenSource();

            // Set up the form
            this.Text = "DI Task & BG Worker Counter Application";
            this.Size = new Size(400, 300);

            var bgControls = InitializeBgControls();
            this.Controls.AddRange(bgControls);

            var taskControls = InitializeTaskControls();
            this.Controls.AddRange(taskControls);
        }

        private Control[] InitializeBgControls()
        {
            var startBgButton = new Button
            {
                Text = "Start BG Counter",
                Location = new Point(20, 20),
                Size = new Size(100, 30)
            };
            startBgButton.Click += (s, e) => _counterService.StartCounting();

            var stopBgButton = new Button
            {
                Text = "Stop BG Counter",
                Location = new Point(130, 20),
                Size = new Size(100, 30)
            };
            stopBgButton.Click += (s, e) => _counterService.StopCounting();

            _counterDisplay.Location = new Point(20, 50);
            _counterDisplay.Size = new Size(200, 30);

            return new Control[] { startBgButton, stopBgButton, _counterDisplay };
        }

        private Control[] InitializeTaskControls()
        {
            var startTaskButton = new Button
            {
                Text = "Start Task Counter",
                Location = new Point(20, 100),
                Size = new Size(100, 30)
            };
            startTaskButton.Click += (s, e) => _taskCounterService.StartCounting(_cancelationToken.Token);

            var stopTaskButton = new Button
            {
                Text = "Stop Task Counter",
                Location = new Point(130, 100),
                Size = new Size(100, 30)
            };
            stopTaskButton.Click += (s, e) => _taskCounterService.StopCounting();

            _taskCounterDisplay.Location = new Point(20, 130);
            _taskCounterDisplay.Size = new Size(200, 30);

            return new Control[] { startTaskButton, stopTaskButton, _taskCounterDisplay };
        }
    }
}
