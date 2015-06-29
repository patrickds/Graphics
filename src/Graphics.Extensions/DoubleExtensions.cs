using System;

namespace Graphics.Extensions
{
    public static class DoubleExtensions
    {
        private const double TOLERANCE = 0.0001d;

        public static bool IsLessOrEqualsTo(this double @this, double that)
        {
            return (@this < that || @this.IsEqualsTo(that));
        }

        public static bool IsGreaterOrEqualsTo(this double @this, double that)
        {
            return (@this > that || @this.IsEqualsTo(that));
        }

        public static bool IsEqualsTo(this double @this, double that)
        {
            return Math.Abs(@this - that) < TOLERANCE;
        }

        public static double Clamp(this double @this, double max)
        {
            return @this < max ? @this : max;
        }
    }
}
