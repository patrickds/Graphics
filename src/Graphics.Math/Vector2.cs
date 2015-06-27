using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Math
{
    public struct Vector2
    {
        public Vector2(double x, double y)
        {
            _x = x;
            _y = y;
            _magnitude = System.Math.Sqrt(x * x + y * y);
        }

        public static Vector2 I = new Vector2(1, 0);
        public static Vector2 J = new Vector2(0, 1);

        private readonly double _x;
        public double X { get{return _x;}}

        private readonly double _y;
        public double Y { get{ return _y;}}

        private readonly double _magnitude;
        public double Magnitude { get { return _magnitude; } }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            double x = v1.X + v2.X;
            double y = v1.Y + v2.Y;

            return new Vector2(x, y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            double x = v1.X - v2.X;
            double y = v1.Y - v2.Y;

            return new Vector2(x, y);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            double x = v1.X * v2.X;
            double y = v1.Y * v2.Y;

            return new Vector2(x, y);
        }
    }
}