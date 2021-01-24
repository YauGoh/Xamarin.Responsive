using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace Xamarin.Responsive.Tests
{
    [Collection(nameof(BaseTests))]
    public class AspectRatioTests : BaseTests
    {



        [Theory]
        [InlineData(4.0, 3.0, 1000.0, 750.0)]
        [InlineData(16.0, 9.0, 1000.0, 562.5)]
        [InlineData(3.0, 2.0, 1000.0, 666.6666666666666)]
        public void ContentShouldBeLaidoutWithCorrectSize(double aspectWidth, double aspectHeight, double expecteWidth, double expectedHeight)
        {
            UseWindowWidth(1000);

            Label label;

            var grid = new Grid
            {
                IsPlatformEnabled = true,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                },
                Children =
                {
                    new AspectRatio
                    {
                        IsPlatformEnabled = true,
                        Ratio = new Size(aspectWidth, aspectHeight),
                        Content = (label = new Label
                        {
                            IsPlatformEnabled = true,
                            Text = "Testing 1, 2, 3"
                        })
                    }
                }
            };

            grid.Layout(new Rectangle(Point.Zero, WindowSize));

            label.Bounds.Size.Should().Be(new Size(expecteWidth, expectedHeight));
        }
    }
}
