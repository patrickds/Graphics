using System;
using System.Diagnostics.Contracts;
using Graphics.Extensions;

namespace Graphics.Math
{
    //Scale it first if needed; then set its direction, then translate it.
    //var transformedVector = translation * rotation * scaling * vector;
    public class Matrix4
    {
        public Matrix4(double m11, double m12, double m13, double m14,
                       double m21, double m22, double m23, double m24,
                       double m31, double m32, double m33, double m34,
                       double m41, double m42, double m43, double m44)
        {
            _m11 = m11; _m12 = m12; _m13 = m13; _m14 = m14;
            _m21 = m21; _m22 = m22; _m23 = m23; _m24 = m24;
            _m31 = m31; _m32 = m32; _m33 = m33; _m34 = m34;
            _m41 = m41; _m42 = m42; _m43 = m43; _m44 = m44;
        }

        private readonly double _m11;
        private readonly double _m12;
        private readonly double _m13;
        private readonly double _m14;
        private readonly double _m21;
        private readonly double _m22;
        private readonly double _m23;
        private readonly double _m24;
        private readonly double _m31;
        private readonly double _m32;
        private readonly double _m33;
        private readonly double _m34;
        private readonly double _m41;
        private readonly double _m42;
        private readonly double _m43;
        private readonly double _m44;

        public static Matrix4 Identity
        {
            get
            {
                return new Matrix4(1, 0, 0, 0,
                                   0, 1, 0, 0,
                                   0, 0, 1, 0,
                                   0, 0, 0, 1);
            }
        }
        
        public static Matrix4 CreateRotation(Vector3 axis, double radians)
        {
            double x = axis.X;
            double y = axis.Y;
            double z = axis.Z;
            double xy = x * y;
            double xz = x * z;
            double yz = y * z;
            double sqrX = x * x;
            double sqrY = y * y;
            double sqrZ = z * z;
            double cos = System.Math.Cos(radians);
            double sin = System.Math.Sin(radians);
            double m11 = sqrX + (cos * (1d - sqrX));
            double m22 = sqrY + (cos * (1d - sqrY));
            double m33 = sqrZ + (cos * (1d - sqrZ));
            double m12 = (xy - (cos * xy)) + (sin * z);
            double m13 = (xz - (cos * xz)) - (sin * y);
            double m21 = (xy - (cos * xy)) - (sin * z);
            double m23 = (yz - (cos * yz)) + (sin * x);
            double m31 = (xz - (cos * xz)) + (sin * y);
            double m32 = (yz - (cos * yz)) - (sin * x);

            return new Matrix4(m11, m12, m13, 0,
                               m21, m22, m23, 0,
                               m31, m32, m33, 0,
                               0, 0, 0, 1);
        }

        public static Matrix4 CreateXRotation(double radians)
        {
            var cos = System.Math.Cos(radians);
            var sin = System.Math.Sin(radians);

            return new Matrix4(0,  0,    0,  0,
                               0, cos, -sin, 0,
                               0, sin,  cos, 0,
                               0,  0,    0,  1);
        }

        public static Matrix4 CreateYRotation(double radians)
        {
            var cos = System.Math.Cos(radians);
            var sin = System.Math.Sin(radians);

            return new Matrix4(cos, 0, -sin, 0,
                                0,  0,   0,  0,
                               sin, 0,  cos, 0,
                                0,  0,   0,  1);
        }

        public static Matrix4 CreateZRotation(double radians)
        {
            var cos = System.Math.Cos(radians);
            var sin = System.Math.Sin(radians);

            return new Matrix4(cos, -sin, 0, 0,
                               sin,  cos, 0, 0,
                                0,    0,  0, 0,
                                0,    0,  0, 1);
        }

        public static Matrix4 CreateTranslation(Vector3 offset)
        {
            return new Matrix4(1, 0, 0, offset.X,
                               0, 1, 0, offset.Y,
                               0, 0, 1, offset.Z,
                               0, 0, 0, 1);
        }

        public static Matrix4 CreateScale(double factor)
        {            
            return CreateScale(new Vector3(factor, factor, factor));
        }

        public static Matrix4 CreateScale(Vector3 factor)
        {
            return new Matrix4(factor.X, 0, 0, 0,
                               0, factor.Y, 0, 0,
                               0, 0, factor.Z, 0,
                               0, 0, 0, 1);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Matrix4))
                return false;

            var matrix = obj as Matrix4;

            return _m11.IsEqualsTo(matrix._m11) &&
                   _m12.IsEqualsTo(matrix._m12) &&
                   _m13.IsEqualsTo(matrix._m13) &&
                   _m14.IsEqualsTo(matrix._m14) &&
                   _m21.IsEqualsTo(matrix._m21) &&
                   _m22.IsEqualsTo(matrix._m22) &&
                   _m23.IsEqualsTo(matrix._m23) &&
                   _m24.IsEqualsTo(matrix._m24) &&
                   _m31.IsEqualsTo(matrix._m31) &&
                   _m32.IsEqualsTo(matrix._m32) &&
                   _m33.IsEqualsTo(matrix._m33) &&
                   _m34.IsEqualsTo(matrix._m34) &&
                   _m41.IsEqualsTo(matrix._m41) &&
                   _m42.IsEqualsTo(matrix._m42) &&
                   _m43.IsEqualsTo(matrix._m43) &&
                   _m44.IsEqualsTo(matrix._m44);
        }

        public override int GetHashCode()
        {
            return (int)(_m11 * 17 + _m12 * 17 + _m13 * 17 + _m14 * 17 +
                         _m21 * 17 + _m22 * 17 + _m23 * 17 + _m24 * 17 +
                         _m31 * 17 + _m32 * 17 + _m33 * 17 + _m34 * 17 +
                         _m41 * 17 + _m42 * 17 + _m43 * 17 + _m44 * 17)+ base.GetHashCode();
        }

        public static Vector4 operator *(Matrix4 m, Vector4 v)
        {
            var x = m._m11 * v.X +
                    m._m12 * v.Y +
                    m._m13 * v.Z +
                    m._m14 * v.W;

            var y = m._m21 * v.X +
                    m._m22 * v.Y +
                    m._m23 * v.Z +
                    m._m24 * v.W;

            var z = m._m31 * v.X +
                    m._m32 * v.Y +
                    m._m33 * v.Z +
                    m._m34 * v.W;

            var w = m._m41 * v.X +
                    m._m42 * v.Y +
                    m._m43 * v.Z +
                    m._m44 * v.W;


            var affineFactor = 1 / 2;

            return new Vector4(x / w, y / w, z / w, w);
        }

        public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
        {
            double m11 = (m1._m11 * m2._m11) + (m1._m12 * m2._m21) + (m1._m13 * m2._m31) + (m1._m14 * m2._m41);
            double m12 = (m1._m11 * m2._m12) + (m1._m12 * m2._m22) + (m1._m13 * m2._m32) + (m1._m14 * m2._m42);
            double m13 = (m1._m11 * m2._m13) + (m1._m12 * m2._m23) + (m1._m13 * m2._m33) + (m1._m14 * m2._m43);
            double m14 = (m1._m11 * m2._m14) + (m1._m12 * m2._m24) + (m1._m13 * m2._m34) + (m1._m14 * m2._m44);

            double m21 = (m1._m21 * m2._m11) + (m1._m22 * m2._m21) + (m1._m23 * m2._m31) + (m1._m24 * m2._m41);
            double m22 = (m1._m21 * m2._m12) + (m1._m22 * m2._m22) + (m1._m23 * m2._m32) + (m1._m24 * m2._m42);
            double m23 = (m1._m21 * m2._m13) + (m1._m22 * m2._m23) + (m1._m23 * m2._m33) + (m1._m24 * m2._m43);
            double m24 = (m1._m21 * m2._m14) + (m1._m22 * m2._m24) + (m1._m23 * m2._m34) + (m1._m24 * m2._m44);

            double m31 = (m1._m31 * m2._m11) + (m1._m32 * m2._m21) + (m1._m33 * m2._m31) + (m1._m34 * m2._m41);
            double m32 = (m1._m31 * m2._m12) + (m1._m32 * m2._m22) + (m1._m33 * m2._m32) + (m1._m34 * m2._m42);
            double m33 = (m1._m31 * m2._m13) + (m1._m32 * m2._m23) + (m1._m33 * m2._m33) + (m1._m34 * m2._m43);
            double m34 = (m1._m31 * m2._m14) + (m1._m32 * m2._m24) + (m1._m33 * m2._m34) + (m1._m34 * m2._m44);

            double m41 = (m1._m41 * m2._m11) + (m1._m42 * m2._m21) + (m1._m43 * m2._m31) + (m1._m44 * m2._m41);
            double m42 = (m1._m41 * m2._m12) + (m1._m42 * m2._m22) + (m1._m43 * m2._m32) + (m1._m44 * m2._m42);
            double m43 = (m1._m41 * m2._m13) + (m1._m42 * m2._m23) + (m1._m43 * m2._m33) + (m1._m44 * m2._m43);
            double m44 = (m1._m41 * m2._m14) + (m1._m42 * m2._m24) + (m1._m43 * m2._m34) + (m1._m44 * m2._m44);

            return new Matrix4(m11, m12, m13, m14,
                               m21, m22, m23, m24,
                               m31, m32, m33, m34,
                               m41, m42, m43, m44);
        }
    }
}