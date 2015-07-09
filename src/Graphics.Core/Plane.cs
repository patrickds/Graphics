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

        public Plane(Vector4 position, Vector3 normal)
            : this (position, normal, DEFAULT_WIDTH, DEFAULT_HEIGHT)
        {
        }

        private const double DEFAULT_WIDTH = 50d;
        private const double DEFAULT_HEIGHT = 50d;

        public Vector3 Normal { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public override void OnRender(DrawingContext drawingContext, Matrix4 transformation, Vector4 cameraPosition)
        {
            base.OnRender(drawingContext, transformation, cameraPosition);
        }
    }
}