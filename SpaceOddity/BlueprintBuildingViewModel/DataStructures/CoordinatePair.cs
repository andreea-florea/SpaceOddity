using Algorithm;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueprintBuildingViewModel.DataStructures
{
    public struct CoordinatePair
    {
        private InterchangeablePair<Coordinate> coordinates;
        public Coordinate First
        {
            get
            {
                return coordinates.First;
            }
        }

        public Coordinate Second
        {
            get
            {
                return coordinates.Second;
            }
        }

        public IEnumerable<Coordinate> Positions
        {
            get
            {
                yield return First;
                yield return Second;
            }
        }

        public CoordinatePair(Coordinate first, Coordinate second)
            : this()
        {
            coordinates = new InterchangeablePair<Coordinate>(first, second);
        }

        public Found<Coordinate> GetCommonPosition(CoordinatePair other)
        {
            foreach (var otherPosition in other.Positions)
            {
                if (Positions.Contains(otherPosition))
                {
                    return new Found<Coordinate>(true, otherPosition);
                }
            }
            return new Found<Coordinate>(false, new Coordinate());
        }

        public static bool operator ==(CoordinatePair a, CoordinatePair b)
        {
            return a.coordinates == b.coordinates;
        }

        public static bool operator !=(CoordinatePair a, CoordinatePair b)
        {
            return a.coordinates != b.coordinates;
        }

        public override bool Equals(object obj)
        {
            var other = (CoordinatePair)obj;
            return coordinates.Equals(other.coordinates);
        }

        public override int GetHashCode()
        {
            return coordinates.GetHashCode();
        }
    }
}
