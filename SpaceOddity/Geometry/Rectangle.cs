using Geometry;
using NaturalNumbersMath;
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

        public Rectangle[,] Split(Coordinate dimensions)
        {
            var splits = new Rectangle[dimensions.Y, dimensions.X];
            var corners = CreateSplitCorners(dimensions);
            var coordinateRectangle = new CoordinateRectangle(Coordinates.Zero, dimensions);
            foreach (var position in coordinateRectangle.Points)
            {
                splits.Set(position, 
                    new Rectangle(corners.Get(position), corners.Get(position + Coordinates.Up + Coordinates.Right)));
            }
            return splits;
        }

        private Vector2[,] CreateSplitCorners(Coordinate dimensions)
        {
            var splitSize = new Vector2(dimensions.X, dimensions.Y);
            var splitDirection = Dimensions.Divide(splitSize);
            var xDirection = splitDirection.XProjection;
            var yDirection = splitDirection.YProjection;

            return CreateMatrixPoints(TopLeftCorner, xDirection, yDirection, 
                dimensions + Coordinates.Up + Coordinates.Right);
        }

        private Vector2[,] CreateMatrixPoints(Vector2 initialPosition, Vector2 xDirection, Vector2 yDirection,
            Coordinate dimensions)
        {
            var corners = new Vector2[dimensions.Y, dimensions.X];

            corners[0, 0] = TopLeftCorner;

            for (var i = 1; i < dimensions.Y; ++i)
            {
                corners[i, 0] = corners[i - 1, 0] + yDirection;
            }

            for (var i = 0; i < dimensions.Y; ++i)
            {
                for (var j = 1; j < dimensions.X; ++j)
                {
                    corners[i, j] = corners[i, j - 1] + xDirection;
                }
            }

            return corners;
        }

    }
}
