using Graphics.Core;
using Graphics.Math;
using System.Collections.Generic;
using System.Windows.Media;

namespace Graphics.Models
{
    public class Cube : Geometry
    {
        private Cube(IEnumerable<Face> faces)
            : base(faces)
        {
        }

        private Cube(IEnumerable<Face> faces, Vector4 position)
            : this(faces)
        {
            this.Position = position;
        }

        public static Cube Default = Create(0.2);

        public static Cube Create(double scaleFactor, Vector4 position)
        {
            var cube = Create(scaleFactor);
            cube.Position = position;

            return cube;
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

            var cube2 = new Cube(new List<Face> { front, back });
            cube2.Transform(Matrix4.CreateScale(scaleFactor));
            return cube2;

            var cube = new Cube(new List<Face>()
                    {
                        bottom, left, front, top, right, back
                    });

            cube.Transform(Matrix4.CreateScale(scaleFactor));

            return cube;
        }
    }
}