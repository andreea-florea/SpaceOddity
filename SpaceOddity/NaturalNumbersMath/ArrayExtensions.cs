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

        public static bool IsWithinBounds<T>(this T[,] array, Coordinate coordinate)
        {
            return coordinate.Y >= 0 && coordinate.X >= 0 &&
                coordinate.Y < array.GetLength(0) && coordinate.X < array.GetLength(1);
        }

        public static int ToInt(this bool[] bits)
        {
            var bitPower = 1;
            var value = 0;
            for (var i = 0; i < bits.Length; ++i)
            {
                value += bits[i] ? bitPower : 0;
                bitPower <<= 1;
            }
            return value;
        }
    }
}
