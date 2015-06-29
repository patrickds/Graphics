using Graphics.Core;
using Graphics.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Models
{
    public class Cube : Entity
    {
        private Cube(IEnumerable<Face> faces)
            : base(faces)
        {
        }

        public static Cube Create(double scaleFactor)
        {
            var front = new Face(new List<Vector4>()
                        {
                            new Vector4(1, -1, -1, 1),
                            new Vector4(-1, -1, -1, 1),
                            new Vector4(-1, 1, -1, 1),
                            new Vector4(1, 1, -1, 1),
                        });

            var back = new Face(new List<Vector4>()
                        {
                            new Vector4(1, -1, 1, 1),
                            new Vector4(-1, -1, 1, 1),
                            new Vector4(-1, 1, 1, 1),
                            new Vector4(1, 1, 1, 1),
                        });

            var top = new Face(new List<Vector4>()
                        {
                            new Vector4(1, 1, -1, 1),
                            new Vector4(-1, 1, -1, 1),
                            new Vector4(-1, 1, 1, 1),
                            new Vector4(1, 1, 1, 1),
                        });

            var bottom = new Face(new List<Vector4>()
                        {
                            new Vector4(1, -1, -1, 1),
                            new Vector4(-1, -1, -1, 1),
                            new Vector4(-1, -1, 1, 1),
                            new Vector4(1, -1, 1, 1),
                        });

            var left = new Face(new List<Vector4>()
                        {
                            new Vector4(-1, -1, -1, 1),
                            new Vector4(-1, -1, 1, 1),
                            new Vector4(-1, 1, 1, 1),
                            new Vector4(-1, 1, -1, 1),
                        });

            var right = new Face(new List<Vector4>()
                        {
                            new Vector4(1, -1, -1, 1),
                            new Vector4(1, -1, 1, 1),
                            new Vector4(1, 1, 1, 1),
                            new Vector4(1, 1, -1, 1),
                        });

            var cube = new Cube(new List<Face>()
                    {
                        front, back, top, bottom, left, right
                    });

            cube.Transform(Matrix4.CreateScale(scaleFactor));

            return cube;
        }
    }
}
