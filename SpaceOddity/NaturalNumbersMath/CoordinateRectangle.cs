using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalNumbersMath
{
    public class CoordinateRectangle
    {
        public Coordinate bottomLeftCorner { get; private set; }
        public Coordinate topRightCorner { get; private set; }

        public IEnumerable<Coordinate> Points
        {
            get
            {
                for (var line = bottomLeftCorner; line.Y < topRightCorner.Y; line += Coordinates.Up)
                {
                    for (var coordinate = line; coordinate.X < topRightCorner.X; coordinate += Coordinates.Right)
                    {
                        yield return coordinate;
                    }
                }
            }
        }

        public CoordinateRectangle(Coordinate bottomLeftCorner, Coordinate topRightCorner)
        {
            this.bottomLeftCorner = bottomLeftCorner;
            this.topRightCorner = topRightCorner;
        }
    }
}
