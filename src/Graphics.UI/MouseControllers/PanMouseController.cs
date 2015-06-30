using Graphics.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Graphics.UI.MouseControllers
{
    internal class PanMouseController : MouseController
    {
        private Point _panStart;

        internal override void ExecuteMouseDown(Viewport sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                sender.Cursor = Cursors.Hand;
                _panStart = e.GetPosition(sender);
            }
        }

        internal override void ExecuteMouseMove(Viewport sender, MouseEventArgs e)
        {
            if (e.MiddleButton != MouseButtonState.Pressed)
                return;

            var mousePosition = e.GetPosition(sender);

            var dx = (mousePosition.X - _panStart.X) / 600;
            var dy = (mousePosition.Y - _panStart.Y) / 600;
            
            _panStart = mousePosition;

            sender.Transform(Matrix4.CreateTranslation(new Vector3(dx, dy, 0)));
        }

        internal override void ExecuteMouseUp(Viewport sender, MouseButtonEventArgs e)
        {
            sender.Cursor = Cursors.Arrow;
        }
    }
}
