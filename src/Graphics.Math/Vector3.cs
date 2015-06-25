using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Math
{
    public struct Vector3
    {
        public Vector3(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public static Vector3 I = new Vector3(1, 0, 0);
        public static Vector3 J = new Vector3(0, 1, 0);
        public static Vector3 K = new Vector3(0, 0, 1);

        private readonly double _x;
        public double X { get{return _x;}}

        private readonly double _y;
        public double Y { get{ return _y;}}

        private readonly double _z;
        public double Z { get { return _z; } }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            double x = v1.X + v2.X;
            double y = v1.Y + v2.Y;
            double z = v1.Z + v2.Z;
            
            return new Vector3(x, y, z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            double x = v1.X - v2.X;
            double y = v1.Y - v2.Y;
            double z = v1.Z - v2.Z;

            return new Vector3(x, y, z);
        }

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            double x = v1.X * v2.X;
            double y = v1.Y * v2.Y;
            double z = v1.Z * v2.Z;

            return new Vector3(x, y, z);
        }
    }
}