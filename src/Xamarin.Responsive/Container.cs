using Xamarin.Forms;

namespace Xamarin.Responsive
{
    public class Container : Layout<Row>
    {
        public static BindableProperty ColumnsProperty = BindableProperty.Create(nameof(Columns), typeof(int), typeof(Container), 12);

        public int Columns { get => (int)GetValue(ColumnsProperty); set => SetValue(ColumnsProperty, value); }

        protected override void LayoutChildren(double x, double y, double width, double height) =>
            PerformLayout(new Rectangle(x, y, width, height), true);


        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint) =>
            new SizeRequest(PerformLayout(
                new Rectangle(0, 0, widthConstraint, heightConstraint),
                false));

        private Size PerformLayout(Rectangle region, bool assignBounds)
        {
            var width = region.Width; // - (Padding.Left + Padding.Right);
            var x = region.X; // + Padding.Left;
            var y = region.Y; // + Padding.Top;

            foreach (var row in Children)
            {
                if (!row.IsVisible) continue;

                row.SetNumberOfColumns(Columns);

                var size = row.Measure(width, double.MaxValue);

                var rowBounds = new Rectangle(x, y, width, size.Minimum.Height);

                if (assignBounds)
                    row.Layout(rowBounds); //.ApplyPadding(row.Padding));

                y += size.Request.Height;
            }

            return new Size(width, (y - region.Y));
        }
    }
}
