using Moq;
using System;
using Xamarin.Forms;

namespace Xamarin.Responsive.Tests
{
    public class MockWindowSizeChangeEmitter : IWindowSizeChangeEmitter
    {
        private readonly Mock<IWindowSizeResolver> _mockWindowSizeResolver;

        internal MockWindowSizeChangeEmitter(Mock<IWindowSizeResolver> mockWindowSizeResolver)
        {
            _mockWindowSizeResolver = mockWindowSizeResolver;
        }

        public event EventHandler<Size> WindowSizeChanged;

        public void SetWindowSize(Size size)
        {
            _mockWindowSizeResolver
                .Setup(w => w.GetCurrentWindowSize())
                .Returns(size);

            WindowSizeChanged?.Invoke(this, size);
        }
    }
}
