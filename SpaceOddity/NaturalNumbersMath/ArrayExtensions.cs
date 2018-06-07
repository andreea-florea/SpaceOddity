using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalNumbersMath
{
    public static class ArrayExtensions
    {
        public static int Width<T>(this T[,] array)
        {
            return array.GetLength(1);
        }

        public static int Height<T>(this T[,] array)
        {
            return array.GetLength(0);
        }

        public static Coordinate Dimensions<T>(this T[,] array)
        {
            return new Coordinate(array.Width(), array.Height());
        }

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
                coordinate.Y < array.Height() && coordinate.X < array.Width();
        }

        public static IEnumerable<Coordinate> GetCoordinates<T>(this T[,] array)
        {
            var rectangle = new CoordinateRectangle(Coordinates.Zero, array.Dimensions());
            foreach (var coordinate in rectangle.Points)
            {
                yield return coordinate;
            }
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
