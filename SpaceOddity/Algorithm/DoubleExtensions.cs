using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public static class DoubleExtensions
    {
        private static double epsilon = 0.000001;

        public static bool CloseTo(this double a, double b)
        {
            return Math.Abs(a - b) < epsilon;
        }

        public static bool SmallerOrEqualTo(this double a, double b)
        {
            return a < b || a.CloseTo(b);
        }
    }
}
