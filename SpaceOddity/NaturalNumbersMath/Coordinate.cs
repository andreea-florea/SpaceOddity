using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalNumbersMath
{
    public struct Coordinate
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coordinate(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }

        public Coordinate RotateQuarterCircleLeft()
        {
            return new Coordinate(Y, -X);
        }

        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return a.X == b.X && b.Y == a.Y;
        }

        public static bool operator!=(Coordinate a, Coordinate b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public static Coordinate operator+(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X + b.X, a.Y + b.Y);
        }

        public static Coordinate operator-(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X - b.X, a.Y - b.Y);
        }

        public static Coordinate operator -(Coordinate a)
        {
            return new Coordinate(-a.X, -a.Y);
        }

        public override bool Equals(object obj)
        {
            return this == (Coordinate)obj;
        }

        public override int GetHashCode()
        {
            return X + Y;
        }
    }
}
