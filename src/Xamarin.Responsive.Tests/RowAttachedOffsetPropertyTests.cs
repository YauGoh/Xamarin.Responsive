using FluentAssertions;
using Xunit;

namespace Xamarin.Responsive.Tests
{
    public class RowAttachedPropertyTests
    {
        [Fact]
        public void ShouldHaveDefaultOffset()
        {
            var mockView = new MockView(20);

            var offset = Row.GetOffset(mockView);

            offset.Xs.Should().BeNull();
            offset.Sm.Should().BeNull();
            offset.Md.Should().BeNull();
            offset.Lg.Should().BeNull();
            offset.Xl.Should().BeNull();
        }

        [Fact]
        public void ShouldSetOffset()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OffsetProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            var offset = Row.GetOffset(mockView);

            offset.Xs.Should().Be(1);
            offset.Sm.Should().Be(2);
            offset.Md.Should().Be(3);
            offset.Lg.Should().Be(4);
            offset.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetOffsetXs()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OffsetProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.OffsetXsProperty, 12);

            var offset = Row.GetOffset(mockView);

            offset.Xs.Should().Be(12);
            offset.Sm.Should().Be(2);
            offset.Md.Should().Be(3);
            offset.Lg.Should().Be(4);
            offset.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetOffsetSm()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OffsetProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.OffsetSmProperty, 12);

            var offset = Row.GetOffset(mockView);

            offset.Xs.Should().Be(1);
            offset.Sm.Should().Be(12);
            offset.Md.Should().Be(3);
            offset.Lg.Should().Be(4);
            offset.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetOffsetMd()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OffsetProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.OffsetMdProperty, 12);

            var offset = Row.GetOffset(mockView);

            offset.Xs.Should().Be(1);
            offset.Sm.Should().Be(2);
            offset.Md.Should().Be(12);
            offset.Lg.Should().Be(4);
            offset.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetOffsetLg()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OffsetProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.OffsetLgProperty, 12);

            var offset = Row.GetOffset(mockView);

            offset.Xs.Should().Be(1);
            offset.Sm.Should().Be(2);
            offset.Md.Should().Be(3);
            offset.Lg.Should().Be(12);
            offset.Xl.Should().Be(5);
        }

        [Fact]
        public void ShouldSetOffsetXl()
        {
            var mockView = new MockView(20);

            mockView.SetValue(Row.OffsetProperty, new ColumnSpecification(xs: 1, sm: 2, md: 3, lg: 4, xl: 5));

            mockView.SetValue(Row.OffsetXlProperty, 12);

            var offset = Row.GetOffset(mockView);

            offset.Xs.Should().Be(1);
            offset.Sm.Should().Be(2);
            offset.Md.Should().Be(3);
            offset.Lg.Should().Be(4);
            offset.Xl.Should().Be(12);
        }
    }
}
