using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalNumbersMath
{
    public class CoordinateRectangle
    {
        public Coordinate TopLeftCorner { get; private set; }
        public Coordinate BottomRightCorner { get; private set; }

        public IEnumerable<Coordinate> Points
        {
            get
            {
                for (var line = TopLeftCorner; line.Y < BottomRightCorner.Y; line += Coordinates.Up)
                {
                    for (var coordinate = line; coordinate.X < BottomRightCorner.X; coordinate += Coordinates.Right)
                    {
                        yield return coordinate;
                    }
                }
            }
        }

        public CoordinateRectangle(Coordinate topLeftCorner, Coordinate bottomRightCorner)
        {
            this.TopLeftCorner = topLeftCorner;
            this.BottomRightCorner = bottomRightCorner;
        }
    }
}
