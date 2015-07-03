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

        private IEnumerable<Line> BuildLines(Matrix4 transformation)
        {
            for (int i = 0; i < this.Points.Count(); i++)
            {
                var current = this.Points.ElementAt(i);
                Vector4 next;

                if (i == this.Points.Count() - 1)
                    next = this.Points.ElementAt(0);
                else
                    next = this.Points.ElementAt(i + 1);

                current = transformation * current;
                next = transformation * next;
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

        public void OnRender(DrawingContext drawingContext, Matrix4 transformation)
        {
            foreach (var line in this.BuildLines(transformation))
            {
                drawingContext.DrawLine(_pen, line.Start, line.End);
            }
        }
    }
}