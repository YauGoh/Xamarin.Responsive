using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Responsive.Handler
{
    internal class WindowSizeChangeHandler
    {
        private IWindowSizeChangeEmitter _emitter;
        private IBreakPointConfigurations _breakPoints;

        private readonly List<Visibility> _visibleAttached;

        public WindowSizeChangeHandler()
        {
            _visibleAttached = new List<Visibility>();
        }

        internal void Register(Visibility view) => _visibleAttached.AddUnique(view);

        internal void Unregister(Visibility view) => _visibleAttached.RemoveAll(_ => _ == view);

        internal void SetBreakPoints(IBreakPointConfigurations breakPoints) => _breakPoints = breakPoints;

        internal void SetEmitter(IWindowSizeChangeEmitter emitter)
        {
            if (_emitter != null)
            {
                _emitter.WindowSizeChanged -= OnWindowSizeChanged;
            }

            _emitter = emitter;

            _emitter.WindowSizeChanged += OnWindowSizeChanged;
        }

        private void OnWindowSizeChanged(object sender, Size size)
        {
            var viewSize = _breakPoints.GetViewSize(size.Width);

            foreach (var visibility in _visibleAttached)
            {
                visibility.ResolveVisiblity(viewSize);
            }
        }
    }
}
