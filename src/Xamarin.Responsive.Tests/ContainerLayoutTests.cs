using Xunit;
using Xamarin.Forms;
using Xamarin.Responsive;
using FluentAssertions;
using System.Collections.Generic;
using System.Diagnostics;

namespace Xamarin.Responsive.Tests
{
    [Collection(nameof(BaseTests))]
    public class ContainerLayoutTests : BaseTests
    {
        [Theory]
        [InlineData(1440, 1, 120, 20)]
        [InlineData(1440, 2, 240, 20)]
        [InlineData(1440, 3, 360, 20)]
        [InlineData(1440, 4, 480, 20)]
        [InlineData(1440, 5, 600, 20)]
        [InlineData(1440, 6, 720, 20)]
        [InlineData(1440, 7, 840, 20)]
        [InlineData(1440, 8, 960, 20)]
        [InlineData(1440, 9, 1080, 20)]
        [InlineData(1440, 10, 1200, 20)]
        [InlineData(1440, 11, 1320, 20)]
        [InlineData(1440, 12, 1440, 20)]
        public void ShouldLayoutBasedOnColumns(double windowWidth, int columns, double expectedViewWidth, double expectedHeight)
        {
            UseWindowWidth(windowWidth);

            Row row;
            var childView = new MockView(expectedHeight);
            childView.SetValue(Row.ColumnProperty, new ColumnSpecification(columns));

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    (row = new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { childView }
                    })
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            row.Bounds.Should().Be(new Rectangle(0, 0, windowWidth, expectedHeight));
            childView.Bounds.Should().Be(new Rectangle(0, 0, expectedViewWidth, expectedHeight));
        }

        [Theory]
        [InlineData(576, 576, 576)]
        [InlineData(768, 384, 384)]
        [InlineData(992, 330.66666666666663, 661.3333333333333)]
        [InlineData(1200, 300, 900)]
        [InlineData(2400, 400, 2000)]
        public void ShouldLayoutBasedOnBreakpoints(double windowWidth, double expectedFirstWidth, double expectedSecondWidth)
        {
            UseWindowWidth(windowWidth);

            Row row;
            var first = new MockView(20);

            first.SetValue(Row.ColumnProperty, new ColumnSpecification(12, 6, 4, 3, 2));

            var second = new MockView(20);
            second.SetValue(Row.ColumnProperty, new ColumnSpecification(12, 6, 8, 9, 10));

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    (row = new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { first, second }
                    })
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            first.Bounds.Width.Should().Be(expectedFirstWidth);
            second.Bounds.Width.Should().Be(expectedSecondWidth);
        }

        [Fact]
        public void ShouldRowHeightShouldBeTallestChildInRow()
        {
            UseMediumDevice();

            Row row;
            var first = new MockView(20);

            first.SetValue(Row.ColumnProperty, new ColumnSpecification(1));

            var second = new MockView(30);
            second.SetValue(Row.ColumnProperty, new ColumnSpecification(1));

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    (row = new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { first, second }
                    })
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            row.Bounds.Height.Should().Be(30);
        }

        [Fact]
        public void ItemsInTheSameRowShouldAllBeHeightOfTheTallestChild()
        {
            UseWindowWidth(600);

            var first = new MockView(20);

            var second = new MockView(30);

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { first, second }
                    }
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            first.Bounds.Should().Be(new Rectangle(0, 0, 50, 30));
            second.Bounds.Should().Be(new Rectangle(50, 0, 50, 30));
        }

        [Fact]
        public void ShouldNotIncludeHiddenChildrenInLayout()
        {
            UseWindowWidth(600);

            Row row;
            var first = new MockView(20)
            {
                IsVisible = false
            };
            first.SetValue(Row.ColumnProperty, new ColumnSpecification(12));

            var second = new MockView(30);
            second.SetValue(Row.ColumnProperty, new ColumnSpecification(12));

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    (row = new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { first, second }
                    })
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            container.ForceLayout();

            first.Bounds.IsEmpty.Should().Be(true);
            second.Bounds.Should().Be(new Rectangle(0, 0, 600, 30));

            row.Bounds.Should().Be(new Rectangle(0, 0, 600, 30));
        }

        [Fact]
        public void SHouldNotIncludeHiddenRows()
        {
            Row row1, row2;
            MockView row1View, row2View;


            UseLargeDevice();

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    (row1 = new Row
                    {
                        IsPlatformEnabled = true,
                        IsVisible = false,
                        Children = { (row1View = new MockView(20)) }
                    }),

                    (row2 = new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { (row2View = new MockView(20)) }
                    })
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            row1View.Bounds.IsEmpty.Should().BeTrue();

            row2View.Bounds.Should().Be(new Rectangle(0, 0, 100.0, 20.0));
        }

        public static IEnumerable<object[]> ContainerHasPaddingData => new object[][]
        {
            new object[] { 1, 1210.0, new Thickness(5), new Rectangle(5,5,1200,20), new Rectangle(0,0,100,20) },
            new object[] { 2, 1210.0, new Thickness(5,0), new Rectangle(5,0,1200,20), new Rectangle(0,0,100,20) },
            new object[] { 3, 1205.0, new Thickness(5,0,0,0), new Rectangle(5,0,1200,20), new Rectangle(0,0,100,20) },
            new object[] { 4, 1205.0, new Thickness(0,0,5,0), new Rectangle(0,0,1200,20), new Rectangle(0,0,100,20) },

            new object[] { 5, 1200.0, new Thickness(0,5), new Rectangle(0,5,1200,20), new Rectangle(0,0,100,20) },
            new object[] { 6, 1200.0, new Thickness(0,5,0,0), new Rectangle(0,5,1200,20), new Rectangle(0,0,100,20) },
            new object[] { 7, 1200.0, new Thickness(0,0,0,5), new Rectangle(0,0,1200,20), new Rectangle(0,0,100,20) },
        };

        [Theory]
        [MemberData(nameof(ContainerHasPaddingData))]
        public void ContainerHasPadding(int scenario, double windowWidth, Thickness thickness, Rectangle expectedRowBounds, Rectangle expectedViewBounds)
        {
            UseWindowWidth(windowWidth);

            Row row;
            MockView view;

            var container = new Container
            {
                Padding = thickness,
                IsPlatformEnabled = true,
                Children =
                {
                    (row = new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { (view =new MockView(20)) }
                    })
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            row.Bounds.Should().Be(expectedRowBounds);
            view.Bounds.Should().Be(expectedViewBounds);
        }

        [Fact]
        public void ContainerWithLargePaddingShouldNotCrash()
        {
            UseWindowWidth(800);

            var container = new Container
            {
                Padding = new Thickness(1000, 1000, 1000, 1000),
                IsPlatformEnabled = true,
                Children =
                {
                    new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { new MockView(2000) }
                    }
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));
        }

        public static IEnumerable<object[]> RowHasPaddingData => new object[][]
        {
            new object[] { 1, 1210.0, new Thickness(5), new Rectangle(0,0,1210,30), new Rectangle(5,5,100,20) },
            new object[] { 2, 1210.0, new Thickness(5,0), new Rectangle(0,0,1210,20), new Rectangle(5,0,100,20) },
            new object[] { 3, 1205.0, new Thickness(5,0,0,0), new Rectangle(0,0,1205,20), new Rectangle(5,0,100,20) },
            new object[] { 4, 1205.0, new Thickness(0,0,5,0), new Rectangle(0,0,1205,20), new Rectangle(0,0,100,20) },

            new object[] { 5, 1200.0, new Thickness(0,5), new Rectangle(0,0,1200,30), new Rectangle(0,5,100,20) },
            new object[] { 6, 1200.0, new Thickness(0,5,0,0), new Rectangle(0,0,1200,25), new Rectangle(0,5,100,20) },
            new object[] { 7, 1200.0, new Thickness(0,0,0,5), new Rectangle(0,0,1200,25), new Rectangle(0,0,100,20) },
        };

        [Theory]
        [MemberData(nameof(RowHasPaddingData))]
        public void RowHasPadding(int scenario, double windowWidth, Thickness thickness, Rectangle expectedRowBounds, Rectangle expectedViewBounds)
        {
            UseWindowWidth(windowWidth);

            Row row;
            MockView view;

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    (row = new Row
                    {
                        Padding = thickness,
                        IsPlatformEnabled = true,
                        Children = { (view =new MockView(20)) }
                    })
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            row.Bounds.Should().Be(expectedRowBounds);
            view.Bounds.Should().Be(expectedViewBounds);
        }

        [Fact]
        public void RowWithLargePaddingShouldNotCrash()
        {
            UseWindowWidth(800);

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    new Row
                    {
                        Padding = new Thickness(1000, 1000, 1000, 1000),
                        IsPlatformEnabled = true,
                        Children = { new MockView(2000) }
                    }
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));
        }

        [Fact]
        public void ShouldWrapToNewRow()
        {
            UseWindowWidth(800);

            var view1 = new MockView(20);
            view1.SetValue(Row.ColumnProperty, new ColumnSpecification(6));

            var view2 = new MockView(33);
            view2.SetValue(Row.ColumnProperty, new ColumnSpecification(6));

            var view3 = new MockView(55);
            view3.SetValue(Row.ColumnProperty, new ColumnSpecification(6));

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { view1, view2, view3 }
                    }
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            view1.Bounds.Should().Be(new Rectangle(0, 0, 400.0, 33.0));
            view2.Bounds.Should().Be(new Rectangle(400.0, 0, 400.0, 33.0));
            view3.Bounds.Should().Be(new Rectangle(0, 33.0, 400.0, 55.0));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 100)]
        [InlineData(2, 200)]
        [InlineData(3, 300)]
        [InlineData(4, 400)]
        [InlineData(5, 500)]
        [InlineData(6, 600)]
        [InlineData(7, 700)]
        [InlineData(8, 800)]
        [InlineData(9, 900)]
        [InlineData(10, 1000)]
        [InlineData(11, 1100)]
        public void ShouldOffset(int offset, double expectedX)
        {
            UseLargeDevice();

            MockView view;

            var container = new Container
            {

                IsPlatformEnabled = true,
                Children =
                {
                    new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { (view = new MockView(20).SetOffset(new ColumnSpecification(lg: offset))) }
                    }
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            view.Bounds.X.Should().Be(expectedX);
        }

        [Theory]
        [InlineData(400, 200)] // xs
        [InlineData(600, 200)] // sm
        [InlineData(900, 225)] // md
        [InlineData(1200, 200)] // lg
        [InlineData(2400, 200)] // lg
        public void ShouldSupportDifferentOffsetsForEachViewSize(double windowWidth, double expectedX)
        {
            UseWindowWidth(windowWidth);

            var view = new MockView(20)
                .SetOffset(new ColumnSpecification(
                    xs: 6,
                    sm: 4,
                    md: 3,
                    lg: 2,
                    xl: 1));

            var container = new Container
            {

                IsPlatformEnabled = true,
                Children =
                {
                    new Row
                    {
                        IsPlatformEnabled = true,
                        Children =
                        {
                            view
                        }
                    }
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            view.Bounds.X.Should().Be(expectedX);
        }


        [Fact]
        public void ShouldWrapColumnsWithOffset()
        {
            UseWindowWidth(1200);

            var view1 = new MockView(20)
                .SetOffset(new ColumnSpecification(3))
                .SetColumn(new ColumnSpecification(3));

            var view2 = new MockView(33)
                .SetColumn(new ColumnSpecification(6));

            var view3 = new MockView(55)
                .SetOffset(new ColumnSpecification(3))
                .SetColumn(new ColumnSpecification(3));

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { view1, view2, view3 }
                    }
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            view1.Bounds.Should().Be(new Rectangle(300, 0, 300, 33.0));
            view2.Bounds.Should().Be(new Rectangle(600, 0, 600, 33.0));
            view3.Bounds.Should().Be(new Rectangle(300, 33.0, 300.0, 55.0));
        }

        [Theory]
        [InlineData(10, 0, 0, 0, 10, 0, 90, 20)]
        [InlineData(0, 10, 0, 0, 0, 10, 100, 30)]
        [InlineData(0, 0, 10, 0, 0, 0, 90, 20)]
        [InlineData(0, 0, 0, 10, 0, 0, 100, 30)]
        [InlineData(10, 5, 10, 5, 10, 5, 80, 30)]
        public void ShouldLayoutChildrenWithMargins(double left, double top, double right, double bottom, double expectedX, double expectedY, double expectedWidth, double expectedRowHeight)
        {
            UseWindowWidth(1200);

            Row row;
            var view = new MockView(20) { Margin = new Thickness(left, top, right, bottom) };

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    (row = new Row
                    {
                        IsPlatformEnabled = true,
                        Children = { view }
                    })
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            row.Bounds.Height.Should().Be(expectedRowHeight);

            view.Bounds.Should().Be(new Rectangle(expectedX, expectedY, expectedWidth, 20));
        }
    }
}
