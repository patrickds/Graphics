using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Core
{
    public class Camera
    {
        public Camera(double near, double far, double aspectRatio)
        {
            this.Position = Vector4.K;
            this.Gaze = new Vector4(0,0,-1,0);
            this.Up = Vector4.J;
            this.Right = Vector4.I;
            this.FoV = 1.919862f; // 110 degrees, close to human FoV
            this.Near = near;
            this.Far = far;
            this.AspectRatio = aspectRatio;

            this.CalculateViewFrustrum();
        }

        public Vector4 Position { get; set; }
        public Vector4 Gaze { get; set; }
        public Vector4 Up { get; set; }
        public Vector4 Right { get; set; }
        public double FoV { get; set; }
        public double AspectRatio { get; set; }
        public double Near { get; set; }
        public double Far { get; set; }
        public Vector3 LeftBottomNear { get; set; }
        public Vector3 RightTopFar { get; set; }

        private void CalculateViewFrustrum()
        {
            double height = System.Math.Tan(this.FoV / 2.0d) * this.Near;
            double width = height * this.AspectRatio;
            double depth = this.Far;

            double halfWidth = width / 2.0d;
            double halfHeight = height / 2.0d;
            double halfDepth = depth / 2.0d;

            this.LeftBottomNear = new Vector3(-halfWidth, -halfHeight, this.Near);
            this.RightTopFar = new Vector3(halfWidth, halfHeight, this.Far);
        }

        private void CalculateNearPlane()
        {
            var angle = this.FoV / 2;
            var halfHeight = (System.Math.Tan(angle) * this.Near);
            var height = halfHeight * 2;
            var width = height * this.AspectRatio;

            var nearCenter = this.Gaze * this.Near;
        }

        private void CalculateFarPlane()
        {
            throw new NotImplementedException();
        }
    }
}