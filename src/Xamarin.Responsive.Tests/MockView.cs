using Xamarin.Forms;

namespace Xamarin.Responsive.Tests
{
    public class MockView : View
    {
        private readonly double _height;

        public MockView(double height)
        {
            IsPlatformEnabled = true;
            _height = height;
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(widthConstraint, _height));
        }
    }
}
