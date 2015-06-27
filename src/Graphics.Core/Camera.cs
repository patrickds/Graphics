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
    }
}