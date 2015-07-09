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
        public string Text { get; set; }
        public IEnumerable<Vector4> Points { get; set; }

        private Vector4 Center()
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

            var centerX = minX + (maxX - minX) / 2;
            var centerY = minY + (maxY - minY) / 2;
            var centerZ = minZ + (maxZ - minZ) / 2;

            return new Vector4(centerX, centerY, centerZ, 1);
        }

        public double DistanceRelativeTo(Vector4 position)
        {
            return (position - this.Center()).Magnitude;
        }

        private PathGeometry BuildGeometry(Matrix4 transformation)
        {
            var segments = new List<PathSegment>();
            var start = transformation * this.Points.First();

            for (int i = 1; i < this.Points.Count(); i++)
            {
                var transformedPoint = transformation * this.Points.ElementAt(i);
                segments.Add(new LineSegment(transformedPoint.ToPoint(), true));
            }

            var pathFigure = new PathFigure(start.ToPoint(), segments, true);
            return new PathGeometry(new List<PathFigure>() { pathFigure });
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
            var faceGeometry = this.BuildGeometry(transformation);

            drawingContext.DrawGeometry(this.Color, _pen, faceGeometry);
        }
    }
}