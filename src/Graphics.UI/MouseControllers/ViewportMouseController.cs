using Graphics.Math;
using Graphics.UI.MouseControllers.Providers;
using System.Windows;
using System.Windows.Input;

namespace Graphics.UI.MouseControllers
{
    internal class ViewportMouseController : MouseController<ViewportControl>
    {
        private const float ROTATION_SUAVIZATION_FACTOR = 0.5f;
        private const float ZOOM_SUAVIZATION_FACTOR = 0.0166f;
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

            sender.Translate(new Vector4(dx, dy, 0, 1));
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

            var xRotation = MathHelper.ToRadians((rotationEnd.X - _moveStart.X)) * ROTATION_SUAVIZATION_FACTOR;
            var yRotation = MathHelper.ToRadians((rotationEnd.Y - _moveStart.Y)) * ROTATION_SUAVIZATION_FACTOR;

            _moveStart = rotationEnd;

            sender.Rotate(xRotation, yRotation);
        }

        private void ExecuteZoom(ViewportControl sender, MouseWheelEventArgs e)
        {
            var mousePosition = e.GetPosition(sender);
            var factor = e.Delta * ZOOM_SUAVIZATION_FACTOR;
            var point = new Vector4(mousePosition.X, mousePosition.Y, 0, 1);

            sender.Zoom(factor);
        }

        internal override void ExecuteMouseWheel(ViewportControl sender, MouseWheelEventArgs e)
        {
            this.ExecuteZoom(sender, e);
        }

        internal override void ExecuteMouseDown(ViewportControl sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.StartPan(sender, e);
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                this.StartRotation(sender, e);
            }
        }

        internal override void ExecuteMouseMove(ViewportControl sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.ExecutePan(sender, e);
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                this.ExecuteRotation(sender, e);
            }
        }

        internal override void ExecuteMouseUp(ViewportControl sender, MouseButtonEventArgs e)
        {
            sender.Cursor = Cursors.Arrow;
        }
    }
}