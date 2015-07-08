using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Graphics.Core
{
    public class Origin : Entity
    {
        public Origin()
        {
            this.Position = new Vector4(0, 0, 0, 1);
        }

        public override void OnRender(DrawingContext drawingContext, Matrix4 transformation, Vector4 cameraPosition)
        {
            //Use this to check affine transformations
            var position = transformation * this.Position;
            drawingContext.DrawEllipse(Brushes.Black, new Pen(), new Point(position.X, position.Y), 2, 2);
        }
    }
}
