using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalNumbersMath
{
    public static class Coordinates
    {
        private static Coordinate[] directions;

        static Coordinates()
        {
            directions = new Coordinate[4];
            directions[0] = Up;
            directions[1] = Right;
            directions[2] = Down;
            directions[3] = Left;
        }

        public static Coordinate[] Directions
        {
            get
            {
                return directions;
            }
        }

        public static Coordinate Zero
        {
            get
            {
                return new Coordinate();
            }
        }

        public static Coordinate Up
        {
            get
            {
                return new Coordinate(0, 1);
            }
        }

        public static Coordinate Right
        {
            get
            {
                return new Coordinate(1, 0);
            }
        }

        public static Coordinate Left
        {
            get
            {
                return new Coordinate(-1, 0);
            }
        }

        public static Coordinate Down
        {
            get
            {
                return new Coordinate(0, -1);
            }
        }
    }
}
