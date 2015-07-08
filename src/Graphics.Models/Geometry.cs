using Graphics.Core;
using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphics.Models
{
    public class Geometry : Entity
    {
        public Geometry(IEnumerable<Face> faces)
        {
            this.Faces = faces;
        }

        public IEnumerable<Face> Faces { get; set; }

        public override void Transform(Matrix4 matrix)
        {
            foreach (var face in this.Faces)
            {
                face.Transform(matrix);
            }
        }

        public override void OnRender(DrawingContext drawingContext, Matrix4 renderTransformation, Vector4 cameraPosition)
        {
            var transformation = renderTransformation;

            foreach (var face in this.Faces.OrderByDescending(f => f.DistanceRelativeTo(cameraPosition)))
            {
                face.OnRender(drawingContext, transformation);
            }
        }
    }
}