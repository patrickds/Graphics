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
            this.Current = new PanMouseController();
        }

        private static MouseControllerManager _instance;
        internal static MouseControllerManager Instance
        {
            get 
            {
                return _instance ?? (_instance = new MouseControllerManager());
            }
        }

        public MouseController Current { get; set; }

        internal void OnMouseUp(Viewport sender, MouseButtonEventArgs e)
        {
            if (this.Current.CanExecuteMouseUp(sender, e))
            {
                this.Current.OnMouseUp(sender, e);
            }
        }

        internal void OnMouseDown(Viewport sender, MouseButtonEventArgs e)
        {
            if (this.Current.CanExecuteMouseDown(sender, e))
            {
                this.Current.OnMouseDown(sender, e);
            }
        }

        internal void OnMouseMove(Viewport sender, MouseEventArgs e)
        {
            if (this.Current.CanExecuteMouseMove(sender, e))
            {
                this.Current.OnMouseMove(sender, e);
            }
        }
    }
}