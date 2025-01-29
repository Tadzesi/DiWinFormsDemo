using DiWinFormsDemo.Custom;
using DiWinFormsDemo.Interfaces;
using Moq;

namespace Tests
{
    public class BgCounterDisplayTests
    {
        private readonly Mock<IBgCounterService> _mockCounterService;
        private BgCounterDisplay _display;

        public BgCounterDisplayTests()
        {
            _mockCounterService = new Mock<IBgCounterService>();
            _display = new BgCounterDisplay(_mockCounterService.Object);
        }

        [Fact]
        public void Constructor_ShouldInitializeWithCurrentValue()
        {
            // Arrange
            var initialValue = 42;
            _mockCounterService.Setup(s => s.CurrentValue).Returns(initialValue);

            // Act
            _display = new BgCounterDisplay(_mockCounterService.Object);

            // Assert
            Assert.Contains(initialValue.ToString(), _display.Text);
        }

        [Fact]
        public void ValueChanged_ShouldUpdateDisplay()
        {
            // Arrange
            var newValue = 10;

            // Act
            _mockCounterService.Raise(s => s.ValueChanged += null, this, newValue);

            // Assert
            Assert.Contains(newValue.ToString(), _display.Text);
        }
    }
}
