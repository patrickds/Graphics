using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Graphics.UI.MouseControllers
{
    internal class ZoomMouseController : MouseController
    {
        internal override void ExecuteMouseWheel(Viewport sender, MouseWheelEventArgs e)
        {
            var factor = e.Delta / 100f;
            sender.Transform(Matrix4.CreateScale(factor));
        }
    }
}
