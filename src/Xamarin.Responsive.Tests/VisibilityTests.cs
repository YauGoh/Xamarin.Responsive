using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace Xamarin.Responsive.Tests
{
    [Collection("WindowSize")]
    public class VisibilityTests : BaseTests
    {
        [Theory]
        [InlineData(576.0)]
        [InlineData(768.0)]
        [InlineData(992.0)]
        [InlineData(1200.0)]
        [InlineData(1600.0)]
        public void ShouldDefaultVisibleToTrue(double width)
        {
            var visibility = new Visibility { Content = new Label { Text = "Testing" } };

            UseWindowWidth(width);

            visibility.IsVisible.Should().Be(true);
        }

        [Theory]
        [InlineData(576.0, false, true, true, true, true)]
        [InlineData(768.0, true, false, true, true, true)]
        [InlineData(992.0, true, true, false, true, true)]
        [InlineData(1200.0, true, true, true, false, true)]
        [InlineData(1600.0, true, true, true, true, false)]
        public void ShouldBeHiddenForAssignedViewSize(double width, bool xs, bool sm, bool md, bool lg, bool xl)
        {
            var visibility = new Visibility { Content = new Label { Text = "Testing" } };

            visibility.Visible =
                new VisibleSpecification()
                {
                    Xs = xs,
                    Sm = sm,
                    Md = md,
                    Lg = lg,
                    Xl = xl
                };

            UseWindowWidth(width);

            visibility.IsVisible.Should().Be(false);
        }

        [Theory]
        [InlineData(576.0, true, false, false, false, false)]
        [InlineData(768.0, false, true, false, false, false)]
        [InlineData(992.0, false, false, true, false, false)]
        [InlineData(1200.0, false, false, false, true, false)]
        [InlineData(1600.0, false, false, false, false, true)]
        public void ShouldBeVisibleForAssignedViewSize(double width, bool xs, bool sm, bool md, bool lg, bool xl)
        {
            var visibility = new Visibility { Content = new Label { Text = "Testing" } };

            visibility.Visible =
                new VisibleSpecification()
                {
                    Xs = xs,
                    Sm = sm,
                    Md = md,
                    Lg = lg,
                    Xl = xl
                };

            UseWindowWidth(width);

            visibility.IsVisible.Should().Be(true);
        }
    }
}
