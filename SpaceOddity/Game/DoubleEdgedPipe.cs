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
    }
}
