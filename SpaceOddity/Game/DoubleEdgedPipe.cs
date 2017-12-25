using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class DoubleEdgedPipe
    {
        public EdgeType FirstEdge { get; private set; }
        public EdgeType SecondEdge { get; private set; }

        public DoubleEdgedPipe(EdgeType firstEdge, EdgeType secondEdge)
        {
            FirstEdge = firstEdge;
            SecondEdge = secondEdge;
        }

        public bool IsEqualTo(DoubleEdgedPipe pipe)
        {
            return ((FirstEdge == pipe.FirstEdge && SecondEdge == pipe.SecondEdge) ||
                (FirstEdge == pipe.SecondEdge && SecondEdge == pipe.FirstEdge));
        }

        public override bool Equals(object obj)
        {
            var pipe = (DoubleEdgedPipe)obj;
            return IsEqualTo(pipe);
        }

        public override int GetHashCode()
        {
            return (int)FirstEdge + (int)SecondEdge;
        }
    }
}
