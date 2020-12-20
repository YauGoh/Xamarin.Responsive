using Xamarin.Forms;

namespace Xamarin.Responsive.Extensions
{
    public static class SizeExtensions
    {
        public static Size AddHeight(this Size size, double height) => new Size(size.Width, size.Height + height);
    }
}
