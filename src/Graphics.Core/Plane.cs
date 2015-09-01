using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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
            this.Color = Brushes.SteelBlue;
            this.GridGap = DEFAULT_GRID_GAP;

            this.BuildPoints();
        }

        public Plane(Vector4 position, Vector3 normal)
            : this(position, normal, DEFAULT_SIZE, DEFAULT_SIZE)
        {
        }

        private const double DEFAULT_SIZE = 50d;
        private const double DEFAULT_GRID_GAP = 10;

        private Pen _pen = new Pen(Brushes.Black, 2);

        public Vector3 Normal { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double GridGap { get; set; }
        public Brush Color { get; set; }
        public IEnumerable<Vector4> Points { get; set; }

        private void BuildPoints()
        {
            var halfWidth = this.Width / 2;
            var halfHeight = this.Height / 2;

            this.Points = new List<Vector4>()
            {
                new Vector4(-halfWidth, 1, -halfHeight, 1),
                new Vector4(-halfWidth, 1, halfHeight, 1),
                new Vector4(halfWidth, 1, halfHeight, 1),
                new Vector4(halfWidth, 1, -halfHeight, 1)
            };
        }

        public override double DistanceRelativeTo(Vector4 position)
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

            var nearest = new Vector4(minX, minY, minZ, 1);
            var farest = new Vector4(maxX, maxY, maxZ, 1);

            return (position - farest).Magnitude;
        }

        public override void OnRender(DrawingContext drawingContext, Matrix4 transformation, Vector4 cameraPosition)
        {
            var geometry = this.BuildGeometry(transformation);

            drawingContext.DrawGeometry(this.Color, _pen, geometry);
        }

        private PathGeometry BuildGeometry(Matrix4 transformation)
        {
            var verticalGridCount = this.Height / this.GridGap;
            var horizontalGridCount = this.Width / this.GridGap;

            var v0 = transformation * this.Points.ElementAt(0);
            var v1 = transformation * this.Points.ElementAt(1);
            var v2 = transformation * this.Points.ElementAt(2);
            var v3 = transformation * this.Points.ElementAt(3);

            var segments = new List<PathSegment>()
                {
                    new LineSegment(v1.ToPoint(), true),
                    new LineSegment(v2.ToPoint(), true),
                    new LineSegment(v3.ToPoint(), true),
                };

            var pathFigure = new PathFigure(v0.ToPoint(), segments, true);
            return new PathGeometry(new List<PathFigure>() { pathFigure });
        }
    }
}