﻿using Graphics.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Math
{
    public struct Vector4
    {
        public Vector4(double x, double y, double z, double w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        //TODO: check W default value for default vectors
        public static Vector4 I = new Vector4(1, 0, 0, 1);
        public static Vector4 J = new Vector4(0, 1, 0, 1);
        public static Vector4 K = new Vector4(0, 0, 1, 1);

        private readonly double _x;
        public double X { get { return _x; } }

        private readonly double _y;
        public double Y { get { return _y; } }

        private readonly double _z;
        public double Z { get { return _z; } }

        private readonly double _w;
        public double W { get { return _w; } }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector4))
                return false;

            var vector = (Vector4)obj;

            return _x.IsEqualsTo(vector.X) &&
                   _y.IsEqualsTo(vector.Y) &&
                   _z.IsEqualsTo(vector.Z) &&
                   _w.IsEqualsTo(vector.W);
        }

        public override int GetHashCode()
        {
            return (int)((((_x * 17) + _y * 17) + _z * 17) + _w * 17) + base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Concat(_x, ", ", _y, ", ", _z, ", ", _w);
        }

        public static Vector4 operator +(Vector4 v1, Vector4 v2)
        {
            double x = v1.X + v2.X;
            double y = v1.Y + v2.Y;
            double z = v1.Z + v2.Z;
            double w = v1.W + v2.W;

            return new Vector4(x, y, z, w);
        }

        public static Vector4 operator -(Vector4 v1, Vector4 v2)
        {
            double x = v1.X - v2.X;
            double y = v1.Y - v2.Y;
            double z = v1.Z - v2.Z;
            double w = v1.W * v2.W;

            return new Vector4(x, y, z, w);
        }

        public static Vector4 operator *(Vector4 v1, Vector4 v2)
        {
            double x = v1.X * v2.X;
            double y = v1.Y * v2.Y;
            double z = v1.Z * v2.Z;
            double w = v1.W * v2.W;

            return new Vector4(x, y, z, w);
        }
    }
}