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
        public Plane(Vector4 position, Vector3 normal, double width, double height)
        {
            this.Position = position;
            this.Normal = normal;
            this.Width = width;
            this.Height = height;
        }

        public Vector3 Normal { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}