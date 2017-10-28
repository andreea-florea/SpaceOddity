using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalNumbersMath
{
    public static class ArrayExtensions
    {
        public static T Get<T>(this T[,] array, Coordinate coordinate)
        {
            return array[coordinate.Y, coordinate.X];
        }

        public static void Set<T>(this T[,] array, Coordinate coordinate, T value)
        {
            array[coordinate.Y, coordinate.X] = value;
        }
    }
}
