using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry
{
    public struct Vector2s
    {
        public static Vector2 Zero
        {
            get
            {
                return new Vector2();
            }
        }

        public static Vector2 Up
        {
            get
            {
                return new Vector2(0, 1);
            }
        }

        public static Vector2 Down
        {
            get
            {
                return new Vector2(0, -1);
            }
        }

        public static Vector2 Right
        {
            get
            {
                return new Vector2(1, 0);
            }
        }

        public static Vector2 Left
        {
            get
            {
                return new Vector2(-1, 0);
            }
        }

        public static Vector2 Diagonal
        {
            get
            {
                return new Vector2(1, 1);
            }
        }
    }
}
