using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Graphics.UI.MouseControllers
{
    internal abstract class MouseController
    {
        internal MouseController()
        {
        }

        internal virtual bool CanExecuteMouseUp(Viewport sender, MouseButtonEventArgs e)
        {
            return true;
        }

        internal virtual void OnMouseUp(Viewport sender, MouseButtonEventArgs e)
        {
        }

        internal virtual bool CanExecuteMouseDown(Viewport sender, MouseButtonEventArgs e)
        {
            return true;
        }

        internal virtual void OnMouseDown(Viewport sender, MouseButtonEventArgs e)
        {
        }

        internal virtual bool CanExecuteMouseWheel(Viewport sender, MouseButtonEventArgs e)
        {
            return true;
        }

        internal virtual void OnMouseWheel(Viewport sender, MouseButtonEventArgs e)
        {
        }

        internal virtual bool CanExecuteMouseMove(Viewport sender, MouseEventArgs e)
        {
            return true;
        }

        internal virtual void OnMouseMove(Viewport sender, MouseEventArgs e)
        {
        }
    }
}