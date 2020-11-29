using System;
using Xamarin.Forms;

namespace Xamarin.Responsive
{
    public interface IWindowSizeChangeEmitter
    {
        event EventHandler<Size> WindowSizeChanged;
    }

    public class CurrentAppWindowSizeChangeEmitter : IWindowSizeChangeEmitter
    {
        public event EventHandler<Size> WindowSizeChanged;

        public CurrentAppWindowSizeChangeEmitter()
        {
            if (Application.Current == null ||
                Application.Current.MainPage == null) return;

            Application.Current.MainPage.SizeChanged += (o, e) =>
            {
                WindowSizeChanged?.Invoke(
                    this,
                    new Size(
                        Application.Current.MainPage.Width,
                        Application.Current.MainPage.Height
                    )
                );
            };
        }
    }
}
