using Xamarin.Forms;

namespace Xamarin.Responsive
{
    internal static class ViewExtensions
    {
        public static TView SetOffset<TView>(this TView view, ColumnSpecification specs)
            where TView : View
        {
            view.SetValue(Row.OffsetProperty, specs);

            return view;
        }

        internal static int GetOffset(this View view, ViewSize viewSize)
        {
            var columnSpecification = (ColumnSpecification)view.GetValue(Row.OffsetProperty);

            switch (viewSize)
            {
                case ViewSize.ExtraSmall:
                    return columnSpecification.Xs ?? 0;

                case ViewSize.Small:
                    return columnSpecification.Sm ?? columnSpecification.Xs ?? 0;

                case ViewSize.Medium:
                    return columnSpecification.Md ?? columnSpecification.Sm ?? columnSpecification.Xs ?? 0;

                case ViewSize.Large:
                    return columnSpecification.Lg ?? columnSpecification.Md ?? columnSpecification.Sm ?? columnSpecification.Xs ?? 0;

                case ViewSize.ExtraLarge:
                    return columnSpecification.Xl ?? columnSpecification.Lg ?? columnSpecification.Md ?? columnSpecification.Sm ?? columnSpecification.Xs ?? 0;
            }

            return 0;
        }

        public static TView SetColumn<TView>(this TView view, ColumnSpecification specs)
            where TView : View
        {
            view.SetValue(Row.ColumnProperty, specs);

            return view;
        }

        internal static int GetColumnCount(this View view, ViewSize viewSize)
        {
            var columnSpecification = (ColumnSpecification)view.GetValue(Row.ColumnProperty);

            switch (viewSize)
            {
                case ViewSize.ExtraSmall:
                    return columnSpecification.Xs ?? 1;

                case ViewSize.Small:
                    return columnSpecification.Sm ?? columnSpecification.Xs ?? 1;

                case ViewSize.Medium:
                    return columnSpecification.Md ?? columnSpecification.Sm ?? columnSpecification.Xs ?? 1;

                case ViewSize.Large:
                    return columnSpecification.Lg ?? columnSpecification.Md ?? columnSpecification.Sm ?? columnSpecification.Xs ?? 1;

                case ViewSize.ExtraLarge:
                    return columnSpecification.Xl ?? columnSpecification.Lg ?? columnSpecification.Md ?? columnSpecification.Sm ?? columnSpecification.Xs ?? 1;
            }

            return 1;
        }

        public static TView SetOrder<TView>(this TView view, OrderSpecification specs)
           where TView : View
        {
            view.SetValue(Row.OrderProperty, specs);

            return view;
        }

        internal static int GetOrder(this View view, ViewSize viewSize)
        {
            var order = (OrderSpecification)view.GetValue(Row.OrderProperty);

            switch (viewSize)
            {
                case ViewSize.ExtraSmall:
                    return order.Xs ?? int.MinValue;

                case ViewSize.Small:
                    return order.Sm ?? order.Xs ?? int.MinValue;

                case ViewSize.Medium:
                    return order.Md ?? order.Sm ?? order.Xs ?? int.MinValue;

                case ViewSize.Large:
                    return order.Lg ?? order.Md ?? order.Sm ?? order.Xs ?? int.MinValue;

                case ViewSize.ExtraLarge:
                    return order.Xl ?? order.Lg ?? order.Md ?? order.Sm ?? order.Xs ?? int.MinValue;
            }

            return int.MinValue;
        }
    }
}
