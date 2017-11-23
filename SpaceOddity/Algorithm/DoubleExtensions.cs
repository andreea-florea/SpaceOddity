using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    public static class DoubleExtensions
    {
        public static bool CloseTo(this double a, double b)
        {
            return Math.Abs(a - b) < 0.000001;
        }
    }
}
