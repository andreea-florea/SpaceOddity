using Algorithm;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.DataStructures
{
    public struct CoordinatePair
    {
        public Coordinate First { get; private set; }
        public Coordinate Second { get; private set; }

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
            First = first;
            Second = second;
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
            return a.First == b.First && a.Second == b.Second ||
                a.First == b.Second && a.Second == b.First;
        }

        public static bool operator !=(CoordinatePair a, CoordinatePair b)
        {
            return (a.First != b.First || a.Second != b.Second) &&
                (a.First != b.Second || a.Second != b.First);
        }

        public override bool Equals(object obj)
        {
            var pair = (CoordinatePair)obj;
            return this == pair;
        }

        public override int GetHashCode()
        {
            return First.GetHashCode() + Second.GetHashCode();
        }
    }
}
