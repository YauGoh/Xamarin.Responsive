using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Responsive
{
    public class OnViewSize<T> : IMarkupExtension<BindingBase>, INotifyPropertyChanged
    {
        private T _xs;
        private bool _xsSet;

        private T _sm;
        private bool _smSet;

        private T _md;
        private bool _mdSet;

        private T _lg;
        private bool _lgSet;

        private T _xl;
        private bool _xlSet;


        public OnViewSize()
        {
            ResponsiveConfiguration.Current.WindowSizeChangeEmitter.WindowSizeChanged += WindowSizeChangeEmitter_WindowSizeChanged;
        }

        ~OnViewSize()
        {
            ResponsiveConfiguration.Current.WindowSizeChangeEmitter.WindowSizeChanged -= WindowSizeChangeEmitter_WindowSizeChanged;
        }

        private void WindowSizeChangeEmitter_WindowSizeChanged(object sender, Forms.Size e)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        public T Value => this;

        public T Default { get; set; }

        public T Xs
        {
            get => _xs;
            set
            {
                _xs = value;
                _xsSet = true;
            }
        }

        public T Sm
        {
            get => _sm;
            set
            {
                _sm = value;
                _smSet = true;
            }
        }

        public T Md
        {
            get => _md;
            set
            {
                _md = value;
                _mdSet = true;
            }
        }

        public T Lg
        {
            get => _lg;
            set
            {
                _lg = value;
                _lgSet = true;
            }
        }

        public T Xl
        {
            get => _xl;
            set
            {
                _xl = value;
                _xlSet = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding(nameof(Value), mode: BindingMode.OneWay, source: this);

            return binding;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ((IMarkupExtension<BindingBase>)this).ProvideValue(serviceProvider);
        }

        public static implicit operator T(OnViewSize<T> onViewSize)
        {
            var viewSize = ResponsiveConfiguration.GetViewSize();

            switch (viewSize)
            {
                case ViewSize.ExtraSmall:
                    return onViewSize._xsSet ? onViewSize.Xs : onViewSize.Default;

                case ViewSize.Small:
                    return onViewSize._smSet ? onViewSize.Sm : onViewSize.Default;

                case ViewSize.Medium:
                    return onViewSize._mdSet ? onViewSize.Md : onViewSize.Default;

                case ViewSize.Large:
                    return onViewSize._lgSet ? onViewSize.Lg : onViewSize.Default;

                case ViewSize.ExtraLarge:
                    return onViewSize._xlSet ? onViewSize.Xl : onViewSize.Default;
            }

            return onViewSize.Default;
        }
    }
}
