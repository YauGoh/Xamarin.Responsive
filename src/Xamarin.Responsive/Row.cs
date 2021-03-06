﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Responsive.Extensions;

namespace Xamarin.Responsive
{
    public class Row : Layout<View>
    {
        int _columns = 12;

        public static readonly BindableProperty ColumnProperty = BindableProperty.CreateAttached("Column", typeof(ColumnSpecification), typeof(Row), new ColumnSpecification());

        public static readonly BindableProperty XsProperty = BindableProperty.CreateAttached("Xs", typeof(int?), typeof(Row), null, propertyChanged: OnXsChanged);

        public static readonly BindableProperty SmProperty = BindableProperty.CreateAttached("Sm", typeof(int?), typeof(Row), null, propertyChanged: OnSmChanged);

        public static readonly BindableProperty MdProperty = BindableProperty.CreateAttached("Md", typeof(int?), typeof(Row), null, propertyChanged: OnMdChanged);

        public static readonly BindableProperty LgProperty = BindableProperty.CreateAttached("Lg", typeof(int?), typeof(Row), null, propertyChanged: OnLgChanged);

        public static readonly BindableProperty XlProperty = BindableProperty.CreateAttached("Xl", typeof(int?), typeof(Row), null, propertyChanged: OnXlChanged);

        public static readonly BindableProperty OffsetProperty = BindableProperty.CreateAttached("Offset", typeof(ColumnSpecification), typeof(Row), new ColumnSpecification());

        public static readonly BindableProperty OffsetXsProperty = BindableProperty.CreateAttached("OffsetXs", typeof(int?), typeof(Row), null, propertyChanged: OnOffsetXsChanged);

        public static readonly BindableProperty OffsetSmProperty = BindableProperty.CreateAttached("OffsetSm", typeof(int?), typeof(Row), null, propertyChanged: OnOffsetSmChanged);

        public static readonly BindableProperty OffsetMdProperty = BindableProperty.CreateAttached("OffsetMd", typeof(int?), typeof(Row), null, propertyChanged: OnOffsetMdChanged);

        public static readonly BindableProperty OffsetLgProperty = BindableProperty.CreateAttached("OffsetLg", typeof(int?), typeof(Row), null, propertyChanged: OnOffsetLgChanged);

        public static readonly BindableProperty OffsetXlProperty = BindableProperty.CreateAttached("OffsetXl", typeof(int?), typeof(Row), null, propertyChanged: OnOffsetXlChanged);

        public static readonly BindableProperty OrderProperty = BindableProperty.CreateAttached(nameof(Order), typeof(OrderSpecification), typeof(Row), new OrderSpecification(), validateValue: ValidateOrder);

        public static readonly BindableProperty OrderXsProperty = BindableProperty.CreateAttached("OrderXs", typeof(int?), typeof(Row), null, propertyChanged: OnOrderXsChanged);

        public static readonly BindableProperty OrderSmProperty = BindableProperty.CreateAttached("OrderSm", typeof(int?), typeof(Row), null, propertyChanged: OnOrderSmChanged);

        public static readonly BindableProperty OrderMdProperty = BindableProperty.CreateAttached("OrderMd", typeof(int?), typeof(Row), null, propertyChanged: OnOrderMdChanged);

        public static readonly BindableProperty OrderLgProperty = BindableProperty.CreateAttached("OrderLg", typeof(int?), typeof(Row), null, propertyChanged: OnOrderLgChanged);

        public static readonly BindableProperty OrderXlProperty = BindableProperty.CreateAttached("OrderXl", typeof(int?), typeof(Row), null, propertyChanged: OnOrderXlChanged);

        public static readonly BindableProperty HeightSpecificationsProperty = BindableProperty.Create(nameof(HeightSpecifications), typeof(ResponsiveRowHieghtSpecification), typeof(Row), new ResponsiveRowHieghtSpecification(), validateValue: ValidateHeight);


        public static ColumnSpecification GetColumn(BindableObject bindable) => (ColumnSpecification)bindable.GetValue(ColumnProperty);

        public static int? GetXs(BindableObject bindable) => (int?)bindable.GetValue(XsProperty);

        public static int? GetSm(BindableObject bindable) => (int?)bindable.GetValue(SmProperty);

        public static int? GetMd(BindableObject bindable) => (int?)bindable.GetValue(MdProperty);

        public static int? GetLg(BindableObject bindable) => (int?)bindable.GetValue(LgProperty);

        public static int? GetXl(BindableObject bindable) => (int?)bindable.GetValue(XlProperty);

        public static ColumnSpecification GetOffset(BindableObject bindable) => (ColumnSpecification)bindable.GetValue(OffsetProperty);

        public static int? GetOffsetXs(BindableObject bindable) => (int?)bindable.GetValue(OffsetXsProperty);

        public static int? GetOffsetSm(BindableObject bindable) => (int?)bindable.GetValue(OffsetSmProperty);

        public static int? GetOffsetMd(BindableObject bindable) => (int?)bindable.GetValue(OffsetMdProperty);

        public static int? GetOffsetLg(BindableObject bindable) => (int?)bindable.GetValue(OffsetLgProperty);

        public static int? GetOffsetXl(BindableObject bindable) => (int?)bindable.GetValue(OffsetXlProperty);


        public static OrderSpecification GetOrder(BindableObject bindable) => (OrderSpecification)bindable.GetValue(OrderProperty);

        public static int? GetOrderXs(BindableObject bindable) => (int?)bindable.GetValue(OrderXsProperty);

        public static int? GetOrderSm(BindableObject bindable) => (int?)bindable.GetValue(OrderSmProperty);

        public static int? GetOrderMd(BindableObject bindable) => (int?)bindable.GetValue(OrderMdProperty);

        public static int? GetOrderLg(BindableObject bindable) => (int?)bindable.GetValue(OrderLgProperty);

        public static int? GetOrderXl(BindableObject bindable) => (int?)bindable.GetValue(OrderXlProperty);


        public ResponsiveRowHieghtSpecification HeightSpecifications
        {
            get => (ResponsiveRowHieghtSpecification)GetValue(HeightSpecificationsProperty);
            set => SetValue(HeightSpecificationsProperty, value);
        }

        public OrderSpecification Order
        {
            get => (OrderSpecification)GetValue(OrderProperty);
            set => SetValue(OrderProperty, value);
        }

        internal RowHeightSpecification GetSpecification(ViewSize viewSize)
        {
            switch (viewSize)
            {
                case ViewSize.ExtraSmall:
                    return HeightSpecifications.Xs ?? new RowHeightSpecification(1, RowHeightUnit.Auto);
                case ViewSize.Small:
                    return HeightSpecifications.Sm ?? HeightSpecifications.Xs ?? new RowHeightSpecification(1, RowHeightUnit.Auto);
                case ViewSize.Medium:
                    return HeightSpecifications.Md ?? HeightSpecifications.Sm ?? HeightSpecifications.Xs ?? new RowHeightSpecification(1, RowHeightUnit.Auto);
                case ViewSize.Large:
                    return HeightSpecifications.Lg ?? HeightSpecifications.Md ?? HeightSpecifications.Sm ?? HeightSpecifications.Xs ?? new RowHeightSpecification(1, RowHeightUnit.Auto);
                case ViewSize.ExtraLarge:
                    return HeightSpecifications.Xl ?? HeightSpecifications.Lg ?? HeightSpecifications.Md ?? HeightSpecifications.Sm ?? HeightSpecifications.Xs ?? new RowHeightSpecification(1, RowHeightUnit.Auto);
                default:
                    return new RowHeightSpecification(1, RowHeightUnit.Auto);
            }
        }

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

            var orderedChildren = GetOrderedChildren(viewSize);

            foreach (var child in orderedChildren)
            {
                if (!child.IsVisible) continue;

                var offset = child.GetOffset(viewSize);

                var offsetRatio = (double)offset / (double)_columns;

                var offsetWidth = offsetRatio * useableWidth;

                currentColumnSpan += offset;

                var columnSpan = child.GetColumnCount(viewSize);

                var ratio = (double)columnSpan / (double)_columns;

                var columnWidth = ratio * useableWidth;

                var sizeRequest = child.Measure(columnWidth - child.Margin.GetWidth(), double.PositiveInfinity);
                var size = sizeRequest.Request.AddHeight(child.Margin.GetHeight());

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

                    maxHeight = size.Height;
                }
                else
                {
                    currentColumnSpan += columnSpan;

                    maxHeight = Math.Max(maxHeight, size.Height);
                }

                var bounds = new Rectangle(currentX + offsetWidth, currentY, columnWidth, size.Height);

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

        private IEnumerable<View> GetOrderedChildren(ViewSize viewSize) => Children.OrderBy(child => child.GetOrder(viewSize)).ToList();

        private void CompleteLayoutFor(List<DeferedLayout> deferedLayouts, double maxHeight)
        {
            foreach (var defered in deferedLayouts)
            {
                defered.AssignLayout(maxHeight);
            }
        }

        private static void OnXsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newXs = (int?)newValue;

            var column = Row.GetColumn(bindable);

            column = column.WithXs(newXs);

            bindable.SetValue(ColumnProperty, column);
        }

        private static void OnSmChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newSm = (int?)newValue;

            var column = Row.GetColumn(bindable);

            column = column.WithSm(newSm);

            bindable.SetValue(ColumnProperty, column);
        }

        private static void OnMdChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newMd = (int?)newValue;

            var column = Row.GetColumn(bindable);

            column = column.WithMd(newMd);

            bindable.SetValue(ColumnProperty, column);
        }

        private static void OnLgChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newLg = (int?)newValue;

            var column = Row.GetColumn(bindable);

            column = column.WithLg(newLg);

            bindable.SetValue(ColumnProperty, column);
        }

        private static void OnXlChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newXl = (int?)newValue;

            var column = Row.GetColumn(bindable);

            column = column.WithXl(newXl);

            bindable.SetValue(ColumnProperty, column);
        }

        private static void OnOffsetXsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newXs = (int?)newValue;

            var offset = Row.GetOffset(bindable);

            offset = offset.WithXs(newXs);

            bindable.SetValue(OffsetProperty, offset);
        }

        private static void OnOffsetSmChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newSm = (int?)newValue;

            var offset = Row.GetOffset(bindable);

            offset = offset.WithSm(newSm);

            bindable.SetValue(OffsetProperty, offset);
        }

        private static void OnOffsetMdChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newMd = (int?)newValue;

            var offset = Row.GetOffset(bindable);

            offset = offset.WithMd(newMd);

            bindable.SetValue(OffsetProperty, offset);
        }

        private static void OnOffsetLgChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newLg = (int?)newValue;

            var offset = Row.GetOffset(bindable);

            offset = offset.WithLg(newLg);

            bindable.SetValue(OffsetProperty, offset);
        }

        private static void OnOffsetXlChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newXl = (int?)newValue;

            var offset = Row.GetOffset(bindable);

            offset = offset.WithXl(newXl);

            bindable.SetValue(OffsetProperty, offset);
        }

        private static void OnOrderXsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newXs = (int?)newValue;

            var order = Row.GetOrder(bindable);

            order = order.WithXs(newXs);

            bindable.SetValue(OrderProperty, order);
        }

        private static void OnOrderSmChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newSm = (int?)newValue;

            var order = Row.GetOrder(bindable);

            order = order.WithSm(newSm);

            bindable.SetValue(OrderProperty, order);
        }

        private static void OnOrderMdChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newMd = (int?)newValue;

            var order = Row.GetOrder(bindable);

            order = order.WithMd(newMd);

            bindable.SetValue(OrderProperty, order);
        }

        private static void OnOrderLgChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newLg = (int?)newValue;

            var order = Row.GetOrder(bindable);

            order = order.WithLg(newLg);

            bindable.SetValue(OrderProperty, order);
        }

        private static void OnOrderXlChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newXl = (int?)newValue;

            var order = Row.GetOrder(bindable);

            order = order.WithXl(newXl);

            bindable.SetValue(OrderProperty, order);
        }

        private static bool ValidateHeight(BindableObject bindable, object value) =>
            value != null &&
            value is ResponsiveRowHieghtSpecification &&
            (((ResponsiveRowHieghtSpecification)value).Xs == null || ((ResponsiveRowHieghtSpecification)value).Xs.Value > 0) &&
            (((ResponsiveRowHieghtSpecification)value).Sm == null || ((ResponsiveRowHieghtSpecification)value).Sm.Value > 0) &&
            (((ResponsiveRowHieghtSpecification)value).Md == null || ((ResponsiveRowHieghtSpecification)value).Md.Value > 0) &&
            (((ResponsiveRowHieghtSpecification)value).Lg == null || ((ResponsiveRowHieghtSpecification)value).Lg.Value > 0) &&
            (((ResponsiveRowHieghtSpecification)value).Xl == null || ((ResponsiveRowHieghtSpecification)value).Xl.Value > 0);

        private static bool ValidateOrder(BindableObject bindable, object value) =>
            value != null &&
            value is OrderSpecification;

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
                _view.Layout(_rectangle
                    .ApplyMargin(_view.Margin)
                    .ApplyHeight(maxHeight - _view.Margin.GetHeight()));
            }
        }
    }
}
