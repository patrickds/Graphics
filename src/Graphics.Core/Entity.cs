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
    public class Entity
    {
        public Entity(IEnumerable<Face> faces)
        {
            this.Faces = faces;
        }

        public Vector4 Position { get; set; }
        public IEnumerable<Face> Faces { get; set; }

        public void Transform(Matrix4 matrix)
        {
            foreach (var face in this.Faces)
            {
                face.Transform(matrix);
            }
        }

        public void OnRender(DrawingContext drawingContext, Matrix4 transformation)
        {
            foreach (var face in this.Faces)
            {
                face.OnRender(drawingContext, transformation);
            }
        }
    }
}
