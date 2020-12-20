using System;
using System.Collections.Generic;
using System.Linq;
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
            var width = region.Width;
            var x = region.X;
            var y = region.Y;

            var heightConstrained = region.Height != double.MaxValue && !double.IsPositiveInfinity(region.Height);
            var deferedLayout = new List<DeferedLayout>();
            var totalFills = 0.0;
            var allocatedHeight = 0.0;

            var viewSize = ResponsiveConfiguration.GetViewSize();

            var orderedChildren = GetOrderedChildren(viewSize);

            foreach (var row in orderedChildren)
            {
                if (!row.IsVisible) continue;

                row.SetNumberOfColumns(Columns);

                var allowHeight = double.MaxValue;

                var heightSpecification = row.GetSpecification(viewSize);

                if (heightSpecification.Unit == RowHeightUnit.Pixels) allowHeight = heightSpecification.Value;

                if (heightSpecification.Unit == RowHeightUnit.Auto)
                {
                    var size = row.Measure(width, allowHeight).Request;

                    deferedLayout.Add(new DeferedLayout(row, DeferedMode.Assigned, size: size));

                    allocatedHeight += size.Height;
                }
                else if (heightSpecification.Unit == RowHeightUnit.Pixels)
                {
                    var size = new Size(width, heightSpecification.Value);

                    deferedLayout.Add(new DeferedLayout(row, DeferedMode.Assigned, size: size));

                    allocatedHeight += size.Height;
                }
                else if (!heightConstrained)
                {
                    var size = row.Measure(width, allowHeight).Request;

                    deferedLayout.Add(new DeferedLayout(row, DeferedMode.Assigned, size: size));

                    allocatedHeight += size.Height;
                }
                else
                {
                    deferedLayout.Add(new DeferedLayout(row, DeferedMode.Fill, fillCount: heightSpecification.Value));

                    totalFills += heightSpecification.Value;
                }
            }

            var unallocatedSpace = totalFills > 0 ? region.Height - allocatedHeight : 0;

            var currentPoint = new Point(x, y);

            foreach (var layout in deferedLayout)
            {
                var rectangle = layout.GetRectangle(currentPoint, width, unallocatedSpace, totalFills);

                if (assignBounds)
                {
                    layout.Apply(rectangle);
                }

                y += rectangle.Height;
                currentPoint = new Point(x, y);
            }

            return new Size(width, heightConstrained ? region.Height : (y - region.Y));
        }

        private IEnumerable<Row> GetOrderedChildren(ViewSize viewSize)
        {
            return Children.OrderBy(row => row.GetOrder(viewSize)).ToList();
        }

        enum DeferedMode
        {
            Assigned,
            Fill
        }

        class DeferedLayout
        {
            private readonly Row _row;
            private readonly DeferedMode _mode;
            private readonly Size? _size;
            private readonly double? _fillCount;

            internal DeferedLayout(Row row, DeferedMode mode, Size? size = null, double? fillCount = null)
            {
                _row = row;
                _mode = mode;
                _size = mode == DeferedMode.Assigned ? (size ?? throw new ArgumentException("Size expected")) : size;
                _fillCount = mode == DeferedMode.Fill ? (fillCount ?? throw new ArgumentException("fill count expected")) : fillCount;
            }

            internal Rectangle GetRectangle(Point current, double width, double unallocatedSpace, double totalFills)
            {
                if (_mode == DeferedMode.Assigned) return new Rectangle(current, _size.Value);

                return new Rectangle(current, new Size(width, ((_fillCount ?? 0) * unallocatedSpace) / totalFills));
            }

            internal void Apply(Rectangle rectangle) => _row.Layout(rectangle);
        }
    }
}
