namespace Xamarin.Responsive
{
    public interface IBreakPointConfigurations
    {
        double Xs { get; }

        double Sm { get; }

        double Md { get; }

        double Lg { get; }
    }

    public class BreakPointConfigurations : IBreakPointConfigurations
    {
        public BreakPointConfigurations() : this(576, 768, 992, 1200) { }

        public BreakPointConfigurations(double xs, double sm, double md, double lg)
        {
            Xs = xs;
            Sm = sm;
            Md = md;
            Lg = lg;
        }

        public double Xs { get; }
        public double Sm { get; }
        public double Md { get; }
        public double Lg { get; }
    }

    public static class BreakPointConfigurationsExtentsions
    {
        internal static ViewSize GetViewSize(this IBreakPointConfigurations breakPoints, double width)
        {
            if (width <= breakPoints.Xs) return ViewSize.ExtraSmall;

            if (width <= breakPoints.Sm) return ViewSize.Small;

            if (width <= breakPoints.Md) return ViewSize.Medium;

            if (width <= breakPoints.Lg) return ViewSize.Large;

            return ViewSize.ExtraLarge;
        }
    }
}
