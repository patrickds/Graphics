using System.Windows.Input;

namespace Graphics.UI.MouseControllers
{
    internal abstract class MouseController<T>
    {
        public eMouseAction MouseAction { get; set; }

        internal MouseController()
        {
        }

        internal virtual bool CanExecuteMouseUp(T sender, MouseButtonEventArgs e)
        {
            return true;
        }

        internal virtual void ExecuteMouseUp(T sender, MouseButtonEventArgs e)
        {
        }

        internal virtual bool CanExecuteMouseDown(T sender, MouseButtonEventArgs e)
        {
            return true;
        }

        internal virtual void ExecuteMouseDown(T sender, MouseButtonEventArgs e)
        {
        }

        internal virtual bool CanExecuteMouseWheel(T sender, MouseWheelEventArgs e)
        {
            return true;
        }

        internal virtual void ExecuteMouseWheel(T sender, MouseWheelEventArgs e)
        {
        }

        internal virtual bool CanExecuteMouseMove(T sender, MouseEventArgs e)
        {
            return true;
        }

        internal virtual void ExecuteMouseMove(T sender, MouseEventArgs e)
        {
        }
    }
}