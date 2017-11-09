using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry
{
    public static class CoordinateExtensions
    {
        public static Vector2 ToVector2(this Coordinate coordinate)
        {
            return new Vector2(coordinate.X, coordinate.Y);
        }
    }
}
