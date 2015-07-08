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

        private Point _moveStart;

        public ViewportMouseController()
        {
            this.MouseAction = eMouseAction.Selection;
        }

        private void StartPan(ViewportControl sender, MouseButtonEventArgs e)
        {
            sender.Cursor = CursorProvider.Cursors[eMouseAction.Pan];
            _moveStart = e.GetPosition(sender);
        }

        private void ExecutePan(ViewportControl sender, MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(sender);

            var dx = mousePosition.X - _moveStart.X;
            var dy = mousePosition.Y - _moveStart.Y;

            _moveStart = mousePosition;

            sender.Translate(Matrix4.CreateTranslation(new Vector3(dx, dy, 0)));
            //sender.Transform(Matrix4.CreateTranslation(new Vector3(dx, dy, 0)));
        }

        private void StartRotation(ViewportControl sender, MouseButtonEventArgs e)
        {
            //TODO: Rotation Cursor
            sender.Cursor = CursorProvider.Cursors[eMouseAction.Pan];
            _moveStart = e.GetPosition(sender);
        }

        private void ExecuteRotation(ViewportControl sender, MouseEventArgs e)
        {
            var rotationEnd = e.GetPosition(sender);

            var xRotation = (rotationEnd.X - _moveStart.X) / 100;
            var yRotation = (rotationEnd.Y - _moveStart.Y) / 100;

            _moveStart = rotationEnd;

            var rotation = Matrix4.CreateYRotation(xRotation);

            sender.Rotate(rotation);
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
            if(e.MiddleButton == MouseButtonState.Pressed)
            {
                if(e.RightButton == MouseButtonState.Pressed)
                {
                    this.StartRotation(sender, e);
                }
                else
                {
                    this.StartPan(sender, e);
                }
            }
        }

        internal override void ExecuteMouseMove(ViewportControl sender, MouseEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    this.ExecuteRotation(sender, e);
                }
                else
                {
                    this.ExecutePan(sender, e);
                }
            }
        }

        internal override void ExecuteMouseUp(ViewportControl sender, MouseButtonEventArgs e)
        {
            sender.Cursor = Cursors.Arrow;
        }
    }
}