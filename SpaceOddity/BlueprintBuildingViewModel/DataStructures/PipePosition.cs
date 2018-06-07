using Algorithms;
using Game.Enums;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueprintBuildingViewModel.DataStructures
{
    public struct PipePosition
    {
        public Coordinate Position { get; private set; }
        private InterchangeablePair<EdgeType> edges;

        public PipePosition(Coordinate position, EdgeType firstEdge, EdgeType secondEdge) : this()
        {
            this.Position = position;
            edges = new InterchangeablePair<EdgeType>(firstEdge, secondEdge);
        }

        public static bool operator ==(PipePosition pipe, PipePosition other)
        {
            return pipe.Position == other.Position && pipe.edges == other.edges;
        }

        public static bool operator !=(PipePosition pipe, PipePosition other)
        {
            return !(pipe == other);
        }

        public override bool Equals(object obj)
        {
            return this == (PipePosition)obj;
        }

        public override int GetHashCode()
        {
            return Position.X + Position.Y + edges.GetHashCode();
        }
    }
}
