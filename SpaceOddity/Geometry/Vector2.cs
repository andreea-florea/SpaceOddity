using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public struct Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2 XProjection
        {
            get
            {
                return new Vector2(X, 0);
            }
        }

        public Vector2 YProjection
        {
            get
            {
                return new Vector2(0, Y);
            }
        }

        public Vector2(double x, double y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2 operator+(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, double scalar)
        {
            return new Vector2(a.X * scalar, a.Y * scalar);
        }

        public static Vector2 operator /(Vector2 a, double scalar)
        {
            return new Vector2(a.X / scalar, a.Y / scalar);
        }

        public static Vector2 operator *(double scalar, Vector2 a)
        {
            return new Vector2(a.X * scalar, a.Y * scalar);
        }

        public Vector2 Divide(Vector2 divider)
        {
            return new Vector2(X / divider.X, Y / divider.Y);
        }
    }
}
