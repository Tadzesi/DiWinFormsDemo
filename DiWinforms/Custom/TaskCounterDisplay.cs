using DiWinFormsDemo.Interfaces;

namespace DiWinFormsDemo.Custom
{
    public class TaskCounterDisplay : Label
    {
        private readonly ITaskCounterService _taskCounterService;

        public TaskCounterDisplay(ITaskCounterService taskCounterService)
        {
            _taskCounterService = taskCounterService;
            this.AutoSize = true;
            this.Font = new Font(Font.FontFamily, 14);
            UpdateDisplay(_taskCounterService.CurrentValue);

            _taskCounterService.ValueChanged += (s, value) => UpdateDisplay(value);
        }

        private void UpdateDisplay(int value)
        {
            this.Text = $"Current Task Count: {value}";
        }
    }
}
