using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace Xamarin.Responsive.Tests
{
    [Collection(nameof(BaseTests))]
    public class RowAttachedOrderPropertyTests : BaseTests
    {
        [Fact]
        public void ShouldHaveDefaultOrder()
        {
            var mockView = new MockView(20);

            var order = Row.GetOrder(mockView);

            order.Xs.Should().BeNull();
            order.Sm.Should().BeNull();
            order.Md.Should().BeNull();
            order.Lg.Should().BeNull();
            order.Xl.Should().BeNull();
        }

        [Fact]
        public void ShouldSetOrder()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OrderProperty, new OrderSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            var order = Row.GetOrder(mockView);

            order.Xs.Should().Be(1);
            order.Sm.Should().Be(2);
            order.Md.Should().Be(3);
            order.Lg.Should().Be(4);
            order.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetOrderXs()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OrderProperty, new OrderSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.OrderXsProperty, 12);

            var order = Row.GetOrder(mockView);

            order.Xs.Should().Be(12);
            order.Sm.Should().Be(2);
            order.Md.Should().Be(3);
            order.Lg.Should().Be(4);
            order.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetOrderSm()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OrderProperty, new OrderSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.OrderSmProperty, 12);

            var order = Row.GetOrder(mockView);

            order.Xs.Should().Be(1);
            order.Sm.Should().Be(12);
            order.Md.Should().Be(3);
            order.Lg.Should().Be(4);
            order.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetOrderMd()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OrderProperty, new OrderSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.OrderMdProperty, 12);

            var order = Row.GetOrder(mockView);

            order.Xs.Should().Be(1);
            order.Sm.Should().Be(2);
            order.Md.Should().Be(12);
            order.Lg.Should().Be(4);
            order.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetOrderLg()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OrderProperty, new OrderSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.OrderLgProperty, 12);

            var order = Row.GetOrder(mockView);

            order.Xs.Should().Be(1);
            order.Sm.Should().Be(2);
            order.Md.Should().Be(3);
            order.Lg.Should().Be(12);
            order.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetOrderXl()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OrderProperty, new OrderSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.OrderXlProperty, 12);

            var order = Row.GetOrder(mockView);

            order.Xs.Should().Be(1);
            order.Sm.Should().Be(2);
            order.Md.Should().Be(3);
            order.Lg.Should().Be(4);
            order.Xl.Should().Be(12);
        }

        [Theory]
        [InlineData(576, 0, 20, 40, 60, 80)]
        [InlineData(768, 20, 0, 40, 60, 80)]
        [InlineData(992, 20, 40, 0, 60, 80)]
        [InlineData(1200, 20, 40, 60, 0, 80)]
        [InlineData(2400, 20, 40, 60, 80, 0)]
        public void ShouldResponsivelyLayoutRowsInOrder(double windowWidth, double expectedRow0Y, double expectedRow1Y, double expectedRow2Y, double expectedRow3Y, double expectedRow4Y)
        {
            Row[] rows = new Row[5];

            UseWindowWidth(windowWidth);

            var container = new Container
            {
                IsPlatformEnabled = true,
                Children =
                {
                    (rows[0] = new Row
                    {
                        IsPlatformEnabled = true,
                        Children =
                        {
                            new MockView(20)
                        }
                    }).SetOrder(new OrderSpecification(1,9,9,9,9)),

                    (rows[1] = new Row
                    {
                        IsPlatformEnabled = true,
                        Children =
                        {
                            new MockView(20)
                        }
                    }).SetOrder(new OrderSpecification(9,1,9,9,9)),

                    (rows[2] = new Row
                    {
                        IsPlatformEnabled = true,
                        Children =
                        {
                            new MockView(20)
                        }
                    }).SetOrder(new OrderSpecification(9,9,1,9,9)),

                    (rows[3] = new Row
                    {
                        IsPlatformEnabled = true,
                        Children =
                        {
                            new MockView(20)
                        }
                    }).SetOrder(new OrderSpecification(9,9,9,1,9)),

                    (rows[4] = new Row
                    {
                        IsPlatformEnabled = true,
                        Children =
                        {
                            new MockView(20)
                        }
                    }).SetOrder(new OrderSpecification(9,9,9,9,1))
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            rows[0].Bounds.Y.Should().Be(expectedRow0Y);
            rows[1].Bounds.Y.Should().Be(expectedRow1Y);
            rows[2].Bounds.Y.Should().Be(expectedRow2Y);
            rows[3].Bounds.Y.Should().Be(expectedRow3Y);
            rows[4].Bounds.Y.Should().Be(expectedRow4Y);
        }


        [Theory]
        [InlineData(576, 0, 48, 96, 144, 192)]
        [InlineData(768, 64, 0, 128, 192, 256)]
        [InlineData(960, 80, 160, 0, 240, 320)]
        [InlineData(1200, 100, 200, 300, 0, 400)]
        [InlineData(2400, 200, 400, 600, 800, 0)]
        public void ShouldResponsivelyLayoutColumnsInOrder(double windowWidth, double expectedColumn0X, double expectedColumn1X, double expectedColumn2X, double expectedColumn3X, double expectedColumn4X)
        {
            MockView[] views = new MockView[5];

            UseWindowWidth(windowWidth);

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
                            (views[0] = new MockView(20).SetOrder(new OrderSpecification(1,9,9,9,9))),
                            (views[1] = new MockView(20).SetOrder(new OrderSpecification(9,2,9,9,9))),
                            (views[2] = new MockView(20).SetOrder(new OrderSpecification(9,9,3,9,9))),
                            (views[3] = new MockView(20).SetOrder(new OrderSpecification(9,9,9,4,9))),
                            (views[4] = new MockView(20).SetOrder(new OrderSpecification(9,9,9,9,5)))
                        }
                    }
                }
            };

            container.Layout(new Rectangle(Point.Zero, WindowSize));

            views[0].Bounds.X.Should().Be(expectedColumn0X);
            views[1].Bounds.X.Should().Be(expectedColumn1X);
            views[2].Bounds.X.Should().Be(expectedColumn2X);
            views[3].Bounds.X.Should().Be(expectedColumn3X);
            views[4].Bounds.X.Should().Be(expectedColumn4X);
        }
    }
}
