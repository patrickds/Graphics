using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Math
{
    public static class MathHelper
    {
        public const int DEFAULT_ROUNDNESS = 6;
        public const double PI = 3.14159265359d;
        private const double ONE_RADIAN_IN_DEGREES = 57.295779513082320876798154814105d;
        private const double ONE_DEGREE_IN_RADIANS = 0.01745329251994329576923690768489d;

        public static double ToRadians(double degrees)
        {
            return degrees * ONE_DEGREE_IN_RADIANS;
        }

        public static double ToDegrees(double radians)
        {
            return radians * ONE_RADIAN_IN_DEGREES;
        }

        public static double Round(double number)
        {
            return System.Math.Round(number, DEFAULT_ROUNDNESS);
        }
    }
}