using Graphics.UI.MouseControllers.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Graphics.UI.MouseControllers
{
    internal class MouseControllerManager
    {
        private MouseControllerManager()
        {
            this.Current = MouseControllerProvider.GetMouseControllerByAction(eMouseAction.Selection);
        }

        private static MouseControllerManager _instance;
        internal static MouseControllerManager Instance
        {
            get 
            {
                return _instance ?? (_instance = new MouseControllerManager());
            }
        }

        public MouseController<Viewport> Current { get; set; }

        internal void OnMouseUp(Viewport sender, MouseButtonEventArgs e)
        {
            if (this.Current.CanExecuteMouseUp(sender, e))
            {
                this.Current.ExecuteMouseUp(sender, e);
            }
        }

        internal void OnMouseDown(Viewport sender, MouseButtonEventArgs e)
        {
            if (this.Current.CanExecuteMouseDown(sender, e))
            {
                this.Current.ExecuteMouseDown(sender, e);
            }
        }

        internal void OnMouseMove(Viewport sender, MouseEventArgs e)
        {
            if (this.Current.CanExecuteMouseMove(sender, e))
            {
                this.Current.ExecuteMouseMove(sender, e);
            }
        }

        internal void OnMouseWheel(Viewport sender, MouseWheelEventArgs e)
        {
            if(this.Current.CanExecuteMouseWheel(sender, e))
            {
                this.Current.ExecuteMouseWheel(sender, e);
            }
        }
    }
}