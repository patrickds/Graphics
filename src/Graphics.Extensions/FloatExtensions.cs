using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics.Extensions
{
    public static class FloatExtensions
    {
        private const double TOLERANCE = 0.0001d;

        public static bool IsLessOrEqualsTo(this float @this, float that)
        {
            return (@this < that || @this.IsEqualsTo(that));
        }

        public static bool IsGreaterOrEqualsTo(this float @this, float that)
        {
            return (@this > that || @this.IsEqualsTo(that));
        }

        public static bool IsEqualsTo(this float @this, float that)
        {
            return Math.Abs(@this - that) < TOLERANCE;
        }

        public static double Clamp(this float @this, float max)
        {
            return @this < max ? @this : max;
        }
    }
}
