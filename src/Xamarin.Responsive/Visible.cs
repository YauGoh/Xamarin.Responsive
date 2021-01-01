using System;
using Xamarin.Forms;

namespace Xamarin.Responsive
{
    [Obsolete("Use OnViewSize xaml extension for setting the IsVisible property instead.")]
    public class Visibility : ContentView
    {
        public static readonly BindableProperty VisibleProperty = BindableProperty.Create(nameof(Visible), typeof(VisibleSpecification), typeof(Visibility), new VisibleSpecification(), propertyChanged: OnVisibilityChanged);

        public VisibleSpecification Visible
        {
            get => (VisibleSpecification)GetValue(VisibleProperty);
            set => SetValue(VisibleProperty, value);
        }

        ~Visibility()
        {
            ResponsiveConfiguration.Current.WindowSizeChangeHandler.Unregister(this);
        }

        internal bool GetVisibility(ViewSize viewSize)
        {

            switch (viewSize)
            {
                case ViewSize.ExtraSmall:
                    return this.Visible.Xs;

                case ViewSize.Small:
                    return this.Visible.Sm;

                case ViewSize.Medium:
                    return this.Visible.Md;

                case ViewSize.Large:
                    return this.Visible.Lg;

                case ViewSize.ExtraLarge:
                    return this.Visible.Xl;
            }

            return true;
        }

        internal void ResolveVisiblity(ViewSize viewSize)
        {
            IsVisible = GetVisibility(viewSize);
        }

        private static void OnVisibilityChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Visibility;

            if (view == null) return;

            if (newValue == default)
                ResponsiveConfiguration.Current.WindowSizeChangeHandler.Unregister(view);
            else
            {
                ResponsiveConfiguration.Current.WindowSizeChangeHandler.Register(view);

                view.ResolveVisiblity(ResponsiveConfiguration.Current.ViewSize);
            }
        }
    }
}
