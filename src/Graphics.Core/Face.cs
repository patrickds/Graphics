using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
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
            this.Color = Brushes.SaddleBrown;
        }

        private const float POINT_RADIUS = 2;
        private Pen _pen = new Pen(Brushes.Black, 1);
        public Brush Color { get; set; }
        public IEnumerable<Vector4> Points { get; set; }
        public Vector4 Center
        {
            get
            {
                double minX = double.MaxValue, minY = double.MaxValue, minZ = double.MaxValue;
                double maxX = double.MinValue, maxY = double.MinValue, maxZ = double.MinValue;

                foreach (var point in this.Points)
                {
                    if (point.X < minX)
                        minX = point.X;
                    else if (point.X > maxX)
                        maxX = point.X;

                    if (point.Y < minY)
                        minY = point.Y;
                    else if (point.Y > maxY)
                        maxY = point.Y;

                    if (point.Z < minZ)
                        minZ = point.Z;
                    else if (point.Z > maxZ)
                        maxZ = point.Z;
                }

                return new Vector4(minX + (maxX - minX), minY + (maxY - minY), minZ + (maxZ - minZ), 1);
            }
        }

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

        public double DistanceRelativeTo(Vector4 position)
        {
            return (position - this.Center).Magnitude;
        }

        //TODO: fix readonly vectors
        public void Transform(Matrix4 matrix)
        {
            var points = new List<Vector4>();

            foreach (var point in this.Points)
            {
                points.Add(matrix * point);
            }

            this.Points = points;
        }

        public void OnRender(DrawingContext drawingContext, Matrix4 transformation)
        {
            var segments = new List<PathSegment>();
            var lines = this.BuildLines(transformation);
            foreach (var line in lines)
            {
                segments.Add(new LineSegment(line.End, true));
                drawingContext.DrawLine(_pen, line.Start, line.End);
            }

            var pathFigure = new PathFigure(lines.First().Start, segments, true);
            drawingContext.DrawGeometry(this.Color, _pen, new PathGeometry(new List<PathFigure>() { pathFigure}));
        }
    }
}