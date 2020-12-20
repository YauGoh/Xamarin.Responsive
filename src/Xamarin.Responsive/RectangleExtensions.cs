using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Responsive.Extensions;

namespace Xamarin.Responsive
{
    public static class RectangleExtensions
    {
        public static Rectangle ApplyPadding(this Rectangle rectangle, Thickness padding)
        {
            return new Rectangle(
                rectangle.X + padding.Left,
                rectangle.Y + padding.Top,
                rectangle.Width - (padding.Left + padding.Right),
                rectangle.Height - (padding.Top + padding.Bottom));
        }

        public static Rectangle ApplyHeight(this Rectangle rectangle, double height)
        {
            return new Rectangle(
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                height);
        }

        public static Rectangle ApplyMargin(this Rectangle rectangle, Thickness thickness) =>
            new Rectangle(
                rectangle.X + thickness.Left,
                rectangle.Y + thickness.Top,
                rectangle.Width - thickness.GetWidth(),
                rectangle.Height - thickness.GetHeight());
    }
}
