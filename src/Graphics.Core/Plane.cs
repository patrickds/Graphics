using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Graphics.Core
{
    public class Plane : Entity
    {
        public Plane(Vector4 position, Vector3 normal)
        {
            this.Position = position;
            this.Normal = normal;
        }

        public Vector3 Normal { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public override void OnRender(DrawingContext drawingContext, Matrix4 transformation)
        {
            base.OnRender(drawingContext, transformation);
        }
    }
}