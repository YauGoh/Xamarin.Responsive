using Xamarin.Forms;

namespace Xamarin.Responsive
{
    public class AspectRatio : ContentView
    {
        public static readonly BindableProperty RatioProperty = BindableProperty.Create(nameof(Ratio), typeof(Size), typeof(AspectRatio), new Size(1, 1));

        public Size Ratio
        {
            get => (Size)GetValue(RatioProperty);
            set => SetValue(RatioProperty, value);
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var ratio = Ratio.Height / Ratio.Width;

            return new SizeRequest(new Size(widthConstraint, widthConstraint * ratio));
        }
    }
}
