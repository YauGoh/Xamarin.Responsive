using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Responsive
{
    public class Row : Layout<View>
    {
        int _columns = 12;

        public static readonly BindableProperty ColumnProperty = BindableProperty.CreateAttached("Column", typeof(ColumnSpecification), typeof(Row), new ColumnSpecification());

        public static readonly BindableProperty OffsetProperty = BindableProperty.CreateAttached("Offset", typeof(ColumnSpecification), typeof(Row), new ColumnSpecification());

        public static ColumnSpecification GetColumn(BindableObject bindable) => (ColumnSpecification)bindable.GetValue(ColumnProperty);

        public static ColumnSpecification GetOffset(BindableObject bindable) => (ColumnSpecification)bindable.GetValue(OffsetProperty);

        protected override void LayoutChildren(double x, double y, double width, double height) => PerformLayout(new Rectangle(x, y, width, height), true);

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint) => new SizeRequest(PerformLayout(new Rectangle(0, 0, widthConstraint, heightConstraint), false));

        internal void SetNumberOfColumns(int columns) => _columns = columns;

        internal Size PerformLayout(Rectangle region, bool assignBounds)
        {
            var maxHeight = 0.0;
            var currentColumnSpan = 0;

            var useableWidth = region.Width;

            var x = region.X;
            var currentX = x;

            var y = region.Y;
            var currentY = y;

            var viewSize = ResponsiveConfiguration.GetViewSize();

            var deferedLayouts = new List<DeferedLayout>();

            foreach (var child in Children)
            {
                if (!child.IsVisible) continue;

                var offset = child.GetOffset(viewSize);

                var offsetRatio = (double)offset / (double)_columns;

                var offsetWidth = offsetRatio * useableWidth;

                currentColumnSpan += offset;

                var columnSpan = child.GetColumnCount(viewSize);

                var ratio = (double)columnSpan / (double)_columns;

                var columnWidth = ratio * useableWidth;

                var size = child.Measure(columnWidth, double.MaxValue);

                if ((currentColumnSpan + columnSpan) > _columns)
                {
                    currentColumnSpan = columnSpan;
                    currentY += maxHeight;
                    currentX = x;

                    if (assignBounds)
                    {
                        CompleteLayoutFor(deferedLayouts, maxHeight);
                        deferedLayouts.Clear();
                    }

                    maxHeight = size.Minimum.Height;
                }
                else
                {
                    currentColumnSpan += columnSpan;

                    maxHeight = Math.Max(maxHeight, size.Minimum.Height);
                }

                var bounds = new Rectangle(currentX + offsetWidth, currentY, columnWidth, size.Minimum.Height);

                if (assignBounds)
                {
                    deferedLayouts.Add(new DeferedLayout(bounds, child));
                }

                currentX += offsetWidth + columnWidth;
            }

            if (assignBounds)
            {
                CompleteLayoutFor(deferedLayouts, maxHeight);
                deferedLayouts.Clear();
            }

            return new Size(region.Width, (currentY - region.Y) + maxHeight);
        }

        private void CompleteLayoutFor(List<DeferedLayout> deferedLayouts, double maxHeight)
        {
            foreach (var defered in deferedLayouts)
            {
                defered.AssignLayout(maxHeight);
            }
        }
    }

    class DeferedLayout
    {
        private readonly Rectangle _rectangle;
        private readonly View _view;

        public DeferedLayout(Rectangle rectangle, View view)
        {
            _rectangle = rectangle;
            _view = view;
        }

        public void AssignLayout(double maxHeight)
        {
            _view.Layout(_rectangle.AppluHeight(maxHeight));
        }
    }
}
