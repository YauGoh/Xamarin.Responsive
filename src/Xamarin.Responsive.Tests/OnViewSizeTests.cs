using FluentAssertions;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Xamarin.Forms.Xaml;
using Xunit;

namespace Xamarin.Responsive.Tests
{
    [Collection("WindowSize")]
    public class OnViewSizeTests : BaseTests
    {
        public OnViewSizeTests()
        {
            MockForms.Init();
        }

        [Theory]
        [InlineData(576, "Extra Small")]
        [InlineData(768, "Small")]
        [InlineData(992, "Medium")]
        [InlineData(1200, "Large")]
        [InlineData(2400, "Extra Large")]
        public void ShouldResolveCorrectValueAssignedToViewSize(double windowWidth, string expected)
        {
            UseWindowWidth(windowWidth);

            var onViewSize = new OnViewSize<string>
            {
                Xs = "Extra Small",
                Sm = "Small",
                Md = "Medium",
                Lg = "Large",
                Xl = "Extra Large"
            };

            string actual = onViewSize;

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(576)]
        [InlineData(768)]
        [InlineData(992)]
        [InlineData(1200)]
        [InlineData(2400)]
        public void ShouldDefaultWhenNotSet(double windowWidth)
        {
            UseWindowWidth(windowWidth);

            var onViewSize = new OnViewSize<string>
            {
                Default = "Default"
            };

            string actual = onViewSize;

            actual.Should().Be("Default");
        }


        [Theory]
        [InlineData(576, "Extra Small")]
        [InlineData(768, "Small")]
        [InlineData(992, "Medium")]
        [InlineData(1200, "Large")]
        [InlineData(2400, "Extra Large")]
        public void ShouldResolveCorrectlyAsMarkupExtension(double windowWidth, string expected)
        {
            UseWindowWidth(windowWidth);

            var label = new Label();

            label.LoadFromXaml(@"
                <Label 
                    xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
                    xmlns:r=""clr-namespace:Xamarin.Responsive;assembly=Xamarin.Responsive"">
                    <Label.Text>
                        <r:OnViewSize
                            x:TypeArguments=""x:String""
                            Lg=""Large""
                            Md=""Medium""
                            Sm=""Small""
                            Xl=""Extra Large""
                            Xs=""Extra Small"" 
                        />
                    </Label.Text>
                </Label>");

            label.Text.Should().Be(expected);
        }

        [Theory]
        [InlineData(2400, "Extra Large", 576, "Extra Small")]
        [InlineData(576, "Extra Small", 768, "Small")]
        [InlineData(576, "Extra Small", 992, "Medium")]
        [InlineData(576, "Extra Small", 1200, "Large")]
        [InlineData(576, "Extra Small", 2400, "Extra Large")]
        public void ShouldRespondToWindowSizeChanges(double initialWindowWidth, string initialText, double resizedWidth, string resizedText)
        {
            UseWindowWidth(initialWindowWidth);

            var label = new Label();

            label.LoadFromXaml(@"
                <Label 
                    xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
                    xmlns:r=""clr-namespace:Xamarin.Responsive;assembly=Xamarin.Responsive"">
                    <Label.Text>
                        <r:OnViewSize
                            x:TypeArguments=""x:String""
                            Lg=""Large""
                            Md=""Medium""
                            Sm=""Small""
                            Xl=""Extra Large""
                            Xs=""Extra Small"" 
                        />
                    </Label.Text>
                </Label>");

            label.Text.Should().Be(initialText);

            UseWindowWidth(resizedWidth);

            label.Text.Should().Be(resizedText);
        }
    }
}
