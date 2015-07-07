using Graphics.Math;
using Graphics.UI.MouseControllers.Providers;
using System.Windows;
using System.Windows.Input;

namespace Graphics.UI.MouseControllers
{
    internal class ViewportMouseController : MouseController<ViewportControl>
    {
        private const double ZOOM_IN_FACTOR = 1.2;
        private const double ZOOM_OUT_FACTOR = 0.8;
        private double _factor = 0.2;

        private Point _panStart;

        public ViewportMouseController()
        {
            this.MouseAction = eMouseAction.Selection;
        }

        private void StartPan(ViewportControl sender, MouseButtonEventArgs e)
        {
            sender.Cursor = CursorProvider.Cursors[eMouseAction.Pan];
            _panStart = e.GetPosition(sender);
        }

        private void ExecutePan(ViewportControl sender, MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(sender);

            var dx = mousePosition.X - _panStart.X;
            var dy = mousePosition.Y - _panStart.Y;

            _panStart = mousePosition;

            sender.Transform(Matrix4.CreateTranslation(new Vector3(dx, dy, 0)));
        }

        private void ExecuteZoom(ViewportControl sender, MouseWheelEventArgs e)
        {
            double factor;
            if (e.Delta < 0)
            {
                factor = ZOOM_OUT_FACTOR;
            }
            else
            {
                factor = ZOOM_IN_FACTOR;
            }

            sender.Transform(Matrix4.CreateScale(factor));
        }

        internal override void ExecuteMouseWheel(ViewportControl sender, MouseWheelEventArgs e)
        {
            this.ExecuteZoom(sender, e);
        }

        internal override void ExecuteMouseDown(ViewportControl sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                StartPan(sender, e);
            }
        }

        internal override void ExecuteMouseMove(ViewportControl sender, MouseEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                this.ExecutePan(sender, e);
            }
        }

        internal override void ExecuteMouseUp(ViewportControl sender, MouseButtonEventArgs e)
        {
            sender.Cursor = Cursors.Arrow;
        }
    }
}