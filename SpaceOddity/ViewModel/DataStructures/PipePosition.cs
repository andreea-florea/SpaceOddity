using Game.Enums;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.DataStructures
{
    public struct PipePosition
    {
        public Coordinate Position { get; private set; }
        public EdgeType FirstEdge { get; private set; }
        public EdgeType SecondEdge { get; private set; }

        public PipePosition(Coordinate position, EdgeType firstEdge, EdgeType secondEdge) : this()
        {
            this.Position = position;
            this.FirstEdge = (EdgeType)(Math.Min((int)firstEdge, (int)secondEdge));
            this.SecondEdge = (EdgeType)(Math.Max((int)firstEdge, (int)secondEdge));
        }

        public static bool operator ==(PipePosition pipe, PipePosition other)
        {
            return pipe.Position == other.Position && 
                pipe.FirstEdge == other.FirstEdge &&
                pipe.SecondEdge == other.SecondEdge;
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
            return Position.X + Position.Y;
        }
    }
}
