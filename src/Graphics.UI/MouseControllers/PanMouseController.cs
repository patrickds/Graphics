using Graphics.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Graphics.UI.MouseControllers
{
    internal class PanMouseController : MouseController
    {
        private double _panStartX;
        private double _panStartY;

        internal override void OnMouseDown(Viewport sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Middle)
                return;

            sender.Cursor = Cursors.Hand;

            var mousePosition = e.GetPosition(sender);

            _panStartX = mousePosition.X;
            _panStartY = mousePosition.Y;
        }

        internal override void OnMouseMove(Viewport sender, MouseEventArgs e)
        {
            if (e.MiddleButton != MouseButtonState.Pressed)
                return;

            var mousePosition = e.GetPosition(sender);

            var dx = mousePosition.X - _panStartX;
            var dy = mousePosition.Y - _panStartY;

            _panStartX = mousePosition.X;
            _panStartY = mousePosition.Y;

            sender.Transform(Matrix4.CreateTranslation(new Vector3(dx, dy, 0)));
        }

        internal override void OnMouseUp(Viewport sender, MouseButtonEventArgs e)
        {
            sender.Cursor = Cursors.Arrow;
        }
    }
}
