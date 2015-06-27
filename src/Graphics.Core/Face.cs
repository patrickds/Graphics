using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Core
{
    public class Face
    {
        public Face(IEnumerable<Vector4> points)
        {
            this.Points = points;
        }

        public IEnumerable<Vector4> Points { get; set; }

        public void Transform(Matrix4 matrix)
        {
            foreach (var point in Points)
            {
                matrix.Transform(point);
            }
        }
    }
}