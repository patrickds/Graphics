using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class DoubleExtensions
    {
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
            return Math.Abs(@this - that) < 0.0001;
        }

        public static double Clamp(this double @this, double max)
        {
            return @this < max ? @this : max;
        }
    }
}
