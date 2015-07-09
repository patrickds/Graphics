using Graphics.Core;
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
    public abstract class Entity
    {
        public Entity()
        {
        }

        public Vector4 Position { get; set; }

        public virtual double DistanceRelativeTo(Vector4 position)
        {
            return (position - this.Position).Magnitude;
        }

        public virtual void Transform(Matrix4 matrix)
        {
            this.Position = matrix * Position;
        }

        public virtual void OnRender(DrawingContext drawingContext, Matrix4 transformation, Vector4 cameraPosition)
        {
        }
    }
}
