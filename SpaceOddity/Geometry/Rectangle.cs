using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Geometry
{
    public struct Rectangle
    {
        public Vector2 TopLeftCorner { get; private set; }
        public Vector2 BottomRightCorner { get; private set; }

        public Vector2 Dimensions
        {
            get
            {
                return BottomRightCorner - TopLeftCorner;
            }
        }

        public Vector2 Center
        {
            get
            {
                return TopLeftCorner + Dimensions * 0.5;
            }
        }

        public Rectangle(Vector2 topLeftCorner, Vector2 bottomRightCorner) : this()
        {
            this.TopLeftCorner = topLeftCorner;
            this.BottomRightCorner = bottomRightCorner;
        }

        public Rectangle[,] Split(int width, int height)
        {
            var splits = new Rectangle[height, width];
            var corners = CreateSplitCorners(width, height);
            for (var i = 0; i < height; ++i)
            {
                for (var j = 0; j < width; ++j)
                {
                    splits[i, j] = new Rectangle(corners[i, j], corners[i + 1, j + 1]);
                }
            }
            return splits;
        }

        private Vector2[,] CreateSplitCorners(int width, int height)
        {
            var splitSize = new Vector2(width, height);
            var splitDirection = Dimensions.Divide(splitSize);
            var xDirection = splitDirection.XProjection;
            var yDirection = splitDirection.YProjection;

            return CreateMatrixPoints(TopLeftCorner, xDirection, yDirection, width + 1, height + 1);
        }

        private Vector2[,] CreateMatrixPoints(Vector2 initialPosition, Vector2 xDirection, Vector2 yDirection,
            int width, int height)
        {
            var corners = new Vector2[height + 1, width + 1];

            corners[0, 0] = TopLeftCorner;

            for (var i = 1; i <= height; ++i)
            {
                corners[i, 0] = corners[i - 1, 0] + yDirection;
            }

            for (var i = 0; i <= height; ++i)
            {
                for (var j = 1; j <= width; ++j)
                {
                    corners[i, j] = corners[i, j - 1] + xDirection;
                }
            }

            return corners;
        }

    }
}
