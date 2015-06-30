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

        internal virtual void ExecuteMouseUp(Viewport sender, MouseButtonEventArgs e)
        {
        }

        internal virtual bool CanExecuteMouseDown(Viewport sender, MouseButtonEventArgs e)
        {
            return true;
        }

        internal virtual void ExecuteMouseDown(Viewport sender, MouseButtonEventArgs e)
        {
        }

        internal virtual bool CanExecuteMouseWheel(Viewport sender, MouseWheelEventArgs e)
        {
            return true;
        }

        internal virtual void ExecuteMouseWheel(Viewport sender, MouseWheelEventArgs e)
        {
        }

        internal virtual bool CanExecuteMouseMove(Viewport sender, MouseEventArgs e)
        {
            return true;
        }

        internal virtual void ExecuteMouseMove(Viewport sender, MouseEventArgs e)
        {
        }
    }
}