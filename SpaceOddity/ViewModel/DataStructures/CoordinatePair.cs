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

        private IEnumerable<Coordinate> Positions
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
    }
}
