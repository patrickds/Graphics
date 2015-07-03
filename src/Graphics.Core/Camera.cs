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
        public Camera(Vector4 position, Vector4 gaze)
        {
            this.Position = position;
            this.Gaze = gaze;
        }

        public Vector4 Position { get; set; }
        public Vector4 Gaze { get; set; }
        public double FoV { get; set; }
        public double AspectRatio { get; set; }
        public double Near { get; set; }
        public double Far { get; set; }


        private void CalculateViewFrustrum()
        {
            var theta = this.FoV / 2;
            var nearHalfHeight = System.Math.Tan(theta) * this.Near;
            var nearHalfWidth = nearHalfHeight * this.AspectRatio;
        }
    }
}