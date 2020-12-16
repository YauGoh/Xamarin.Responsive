using FluentAssertions;
using Xunit;

namespace Xamarin.Responsive.Tests
{
    public class RowAttachedColumnPropertyTests
    {
        [Fact]
        public void ShouldHaveDefaultValue()
        {
            var mockView = new MockView(20);

            var column = Row.GetColumn(mockView);

            column.Xs.Should().BeNull();
            column.Sm.Should().BeNull();
            column.Md.Should().BeNull();
            column.Lg.Should().BeNull();
            column.Xl.Should().BeNull();
        }

        [Fact]
        public void ShouldSetColumn()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.ColumnProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            var column = Row.GetColumn(mockView);

            column.Xs.Should().Be(1);
            column.Sm.Should().Be(2);
            column.Md.Should().Be(3);
            column.Lg.Should().Be(4);
            column.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetXs()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.ColumnProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.XsProperty, 12);

            var column = Row.GetColumn(mockView);

            column.Xs.Should().Be(12);
            column.Sm.Should().Be(2);
            column.Md.Should().Be(3);
            column.Lg.Should().Be(4);
            column.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetSm()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.ColumnProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.SmProperty, 12);

            var column = Row.GetColumn(mockView);

            column.Xs.Should().Be(1);
            column.Sm.Should().Be(12);
            column.Md.Should().Be(3);
            column.Lg.Should().Be(4);
            column.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetMd()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.ColumnProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.MdProperty, 12);

            var column = Row.GetColumn(mockView);

            column.Xs.Should().Be(1);
            column.Sm.Should().Be(2);
            column.Md.Should().Be(12);
            column.Lg.Should().Be(4);
            column.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetLg()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.ColumnProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.LgProperty, 12);

            var column = Row.GetColumn(mockView);

            column.Xs.Should().Be(1);
            column.Sm.Should().Be(2);
            column.Md.Should().Be(3);
            column.Lg.Should().Be(12);
            column.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetXl()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.ColumnProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.XlProperty, 12);

            var column = Row.GetColumn(mockView);

            column.Xs.Should().Be(1);
            column.Sm.Should().Be(2);
            column.Md.Should().Be(3);
            column.Lg.Should().Be(4);
            column.Xl.Should().Be(12);
        }
    }
}
