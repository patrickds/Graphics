using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Math
{
    public class Matrix4
    {
        private Matrix4(double m11, double m12, double m13, double m14,
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

        public static Matrix4 CreateRotation(Vector3 axis, double angle)
        {
            throw new NotImplementedException();
        }
        //TODO: check whether W is necessary and replace for Vector3
        public static Matrix4 CreateTranslation(Vector3 offset)
        {
            return new Matrix4(1, 0, 0, offset.X,
                               0, 1, 0, offset.Y,
                               0, 0, 1, offset.Z,
                               0, 0, 0, 1);
        }

        public static Matrix4 CreateScale(Vector3 factor)
        {
            return new Matrix4(factor.X, 0, 0, 0,
                               0, factor.Y, 0, 0,
                               0, 0, factor.Z, 0,
                               0, 0, 0, 1);
        }

        public static Vector4 operator *(Matrix4 matrix, Vector4 vector)
        {
            var x = matrix._m11 * vector.X +
                    matrix._m12 * vector.Y +
                    matrix._m13 * vector.Z +
                    matrix._m14 * vector.W;

            var y = matrix._m21 * vector.X +
                    matrix._m22 * vector.Y +
                    matrix._m23 * vector.Z +
                    matrix._m24 * vector.W;

            var z = matrix._m31 * vector.X +
                    matrix._m32 * vector.Y +
                    matrix._m33 * vector.Z +
                    matrix._m34 * vector.W;

            var w = matrix._m41 * vector.X +
                    matrix._m42 * vector.Y +
                    matrix._m43 * vector.Z +
                    matrix._m44 * vector.W;

            return new Vector4(x, y, z, w);
        }
    }
}
