using DiWinFormsDemo.Interfaces;

namespace DiWinFormsDemo.Custom
{
    public class BgCounterDisplay : Label
    {
        private readonly IBgCounterService _counterService;

        public BgCounterDisplay(IBgCounterService counterService)
        {
            _counterService = counterService;
            this.AutoSize = true;
            this.Font = new Font(Font.FontFamily, 14);
            UpdateDisplay(_counterService.CurrentValue);

            _counterService.ValueChanged += (s, value) =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => UpdateDisplay(value)));
                }
                else
                {
                    UpdateDisplay(value);
                }
            };
        }

        private void UpdateDisplay(int value)
        {
            this.Text = $"Current BG worker Count: {value}";
        }
    }
}
