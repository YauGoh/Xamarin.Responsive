using System;
using Xamarin.Responsive.Handler;

namespace Xamarin.Responsive
{
    public class ResponsiveConfiguration
    {
        static WindowSizeChangeHandler windowSizeChangedHandler = new WindowSizeChangeHandler();

        static ResponsiveConfiguration()
        {
            Current = new ResponsiveConfiguration();
        }

        internal ResponsiveConfiguration() :
            this(new BreakPointConfigurations(),
                new CurrentAppWindowSizeResolver(),
                new CurrentAppWindowSizeChangeEmitter())
        { }

        internal ResponsiveConfiguration(
            IBreakPointConfigurations breakPoints,
            IWindowSizeResolver windowSize,
            IWindowSizeChangeEmitter windowSizeChanged)
        {
            BreakPoints = breakPoints;
            WindowSizeResolver = windowSize;
            WindowSizeChangeEmitter = windowSizeChanged;

            windowSizeChangedHandler.SetBreakPoints(breakPoints);
            windowSizeChangedHandler.SetEmitter(windowSizeChanged);
        }

        internal WindowSizeChangeHandler WindowSizeChangeHandler => windowSizeChangedHandler;

        internal IBreakPointConfigurations BreakPoints { get; }

        internal IWindowSizeResolver WindowSizeResolver { get; }

        internal IWindowSizeChangeEmitter WindowSizeChangeEmitter { get; }

        internal ViewSize ViewSize
        {
            get
            {
                var size = WindowSizeResolver.GetCurrentWindowSize();

                return BreakPoints.GetViewSize(size.Width);
            }
        }

        internal static ResponsiveConfiguration Current { get; private set; }

        public static ResponsiveConfiguration UseBreakPoints(IBreakPointConfigurations breakPoints) => Current = new ResponsiveConfiguration(breakPoints, Current.WindowSizeResolver, Current.WindowSizeChangeEmitter);

        internal static ResponsiveConfiguration UseWindowSizeResolver(IWindowSizeResolver windowSizeResolver) => Current = new ResponsiveConfiguration(Current.BreakPoints, windowSizeResolver, Current.WindowSizeChangeEmitter);

        internal static ResponsiveConfiguration UseWindowSizeEmitter(IWindowSizeChangeEmitter windowSizeChangeEmitter) => Current = new ResponsiveConfiguration(Current.BreakPoints, Current.WindowSizeResolver, windowSizeChangeEmitter);

        internal static ViewSize GetViewSize()
        {
            var width = Current.WindowSizeResolver.GetCurrentWindowSize().Width;

            return Current.BreakPoints.GetViewSize(width);
        }
    }
}
