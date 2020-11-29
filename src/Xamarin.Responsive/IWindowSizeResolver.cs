using Xamarin.Forms;

namespace Xamarin.Responsive
{
    internal interface IWindowSizeResolver
    {
        Size GetCurrentWindowSize();
    }

    internal class CurrentAppWindowSizeResolver : IWindowSizeResolver
    {
        public Size GetCurrentWindowSize() => new Size(Application.Current.MainPage.Width, Application.Current.MainPage.Height);
    }
}
