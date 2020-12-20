using Xamarin.Forms;

namespace Xamarin.Responsive.Extensions
{
    public static class ThicknessExtensions
    {
        public static double GetWidth(this Thickness thickness) => thickness.Left + thickness.Right;

        public static double GetHeight(this Thickness thickness) => thickness.Top + thickness.Bottom;

        public static Size GetOffset(this Thickness thickness) => new Size(thickness.Left, thickness.Top);
    }
}
