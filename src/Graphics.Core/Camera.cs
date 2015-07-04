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
        public Camera()
        {
            this.Position = new Vector4(0,0,0,1);
            this.Gaze = new Vector4(0,0,-1,0);
            this.Up = Vector4.J;
            this.Side = Vector4.I;
            this.FoV = 1.919862f; // 110 degrees, close to human FoV
            this.Near = 3;
            this.Far = 100;
        }

        public Vector4 Position { get; set; }
        public Vector4 Gaze { get; set; }
        public Vector4 Up { get; set; }
        public Vector4 Side { get; set; }
        public double FoV { get; set; }
        public double AspectRatio { get; set; }
        public double Near { get; set; }
        public double Far { get; set; }

        private void CalculateViewFrustrum()
        {
            this.CalculateNearPlane();
            this.CalculateFarPlane();
        }

        private void CalculateNearPlane()
        {
            var angle = this.FoV / 2;
            var halfHeight = (System.Math.Tan(angle) * this.Near);
            var height = halfHeight * 2;
            var width = height * this.AspectRatio;

            var nearCenter = this.Gaze * this.Near;

            var nearTopLeft = new Vector4();

        }

        private void CalculateFarPlane()
        {
            throw new NotImplementedException();
        }
    }
}