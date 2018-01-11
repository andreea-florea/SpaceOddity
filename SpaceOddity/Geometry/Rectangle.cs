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
        public Vector2 BottomLeftCorner { get; private set; }
        public Vector2 TopRightCorner { get; private set; }

        public Vector2 Dimensions
        {
            get
            {
                return TopRightCorner - BottomLeftCorner;
            }
        }

        public Vector2 Center
        {
            get
            {
                return BottomLeftCorner + Dimensions * 0.5;
            }
        }

        public Rectangle(Vector2 bottomLeftCorner, Vector2 topRightCorner) : this()
        {
            this.BottomLeftCorner = bottomLeftCorner;
            this.TopRightCorner = topRightCorner;
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

            return CreateMatrixPoints(BottomLeftCorner, xDirection, yDirection, 
                dimensions + Coordinates.Up + Coordinates.Right);
        }

        private Vector2[,] CreateMatrixPoints(Vector2 initialPosition, Vector2 xDirection, Vector2 yDirection,
            Coordinate dimensions)
        {
            var corners = new Vector2[dimensions.Y, dimensions.X];
            var splitsRectangle = new CoordinateRectangle(Coordinates.Zero, dimensions);

            foreach (var coordinate in splitsRectangle.Points)
            {
                corners.Set(coordinate, initialPosition + coordinate.X * xDirection + coordinate.Y * yDirection);
            }
            return corners;
        }

    }
}
