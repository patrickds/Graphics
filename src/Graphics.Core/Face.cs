using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Graphics.Core
{
    public class Face
    {
        public Face(IEnumerable<Vector4> points)
        {
            Contract.Requires<ArgumentException>(points.Count() > 2, "Cannot build a face with less than 3 points");

            this.Points = points;
        }

        private const float POINT_RADIUS = 2;
        private Pen _pen = new Pen(Brushes.Black, 1);
        public IEnumerable<Vector4> Points { get; set; }

        private IEnumerable<Line> BuildLines()
        {
            for (int i = 0; i < this.Points.Count(); i++)
            {
                var current = this.Points.ElementAt(i);
                Vector4 next;

                if (i == this.Points.Count() - 1)
                    next = this.Points.ElementAt(0);
                else
                    next = this.Points.ElementAt(i + 1);

                yield return new Line(new Point(current.X, current.Y), new Point(next.X, next.Y));
            }
        }

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
            foreach (var line in this.BuildLines())
            {
                drawingContext.DrawLine(_pen, line.Start, line.End);
            }
        }
    }
}