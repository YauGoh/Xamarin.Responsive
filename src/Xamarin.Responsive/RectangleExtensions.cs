using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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

        public static Rectangle AppluHeight(this Rectangle rectangle, double height)
        {
            return new Rectangle(
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                height);
        }
    }
}
