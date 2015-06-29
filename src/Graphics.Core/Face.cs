using Graphics.Math;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Graphics.Core
{
    public class Face
    {
        public Face(IEnumerable<Vector4> points)
        {
            this.Points = points;
        }

        private const float POINT_RADIUS = 2;

        public IEnumerable<Vector4> Points { get; set; }

        private Pen _pen = new Pen(Brushes.Black, 1);

        //TODO: fix readonly vectors
        public void Transform(Matrix4 matrix)
        {
            var points = new List<Vector4>();

            foreach (var point in this.Points)
            {
                points.Add(matrix.Transform(point));
            }

            this.Points = points;
        }

        internal void OnRender(DrawingContext drawingContext)
        {
            for (int i = 0; i < this.Points.Count(); i++)
            {
                var current = this.Points.ElementAt(i);
                Vector4 next;
                
                if (i == this.Points.Count() - 1)
                    next = this.Points.ElementAt(0);
                else
                    next = this.Points.ElementAt(i + 1);

                var currentPoint = new Point(current.X, current.Y);
                var nextPoint = new Point(next.X, next.Y);

                drawingContext.DrawLine(_pen, currentPoint, nextPoint);

                //drawingContext.DrawEllipse(
                //    Brushes.Black,
                //    _pen,
                //    currentPoint,
                //    POINT_RADIUS,
                //    POINT_RADIUS);
            }
        }
    }
}