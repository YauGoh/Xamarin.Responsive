using Moq;
using Xamarin.Forms;

namespace Xamarin.Responsive.Tests
{
    public class BaseTests
    {
        private MockWindowSizeChangeEmitter _mockWindowSizeEmitter;

        public BaseTests()
        {
            MockWindowSizeResolver = new Mock<IWindowSizeResolver>();
            MockWindowSizeResolver.Setup(s => s.GetCurrentWindowSize()).Returns(new Size(800, 600));

            _mockWindowSizeEmitter = new MockWindowSizeChangeEmitter(MockWindowSizeResolver);

            ResponsiveConfiguration.UseWindowSizeResolver(MockWindowSizeResolver.Object);
            ResponsiveConfiguration.UseWindowSizeEmitter(_mockWindowSizeEmitter);

        }

        internal Mock<IWindowSizeResolver> MockWindowSizeResolver { get; }

        internal Mock<IWindowSizeChangeEmitter> MockWindowSizeChangeEmitter { get; }

        internal Size WindowSize => MockWindowSizeResolver.Object.GetCurrentWindowSize();

        internal void UseWindowWidth(double windowWidth)
        {
            _mockWindowSizeEmitter.SetWindowSize(new Size(windowWidth, 600));
        }

        internal void UseExtraSmallDevice() => UseWindowWidth(576.0);

        internal void UseSmallDevice() => UseWindowWidth(768.0);

        internal void UseMediumDevice() => UseWindowWidth(992.0);

        internal void UseLargeDevice() => UseWindowWidth(1200.0);

        internal void UseExtraLargeDevice() => UseWindowWidth(1600.0);
    }
}
