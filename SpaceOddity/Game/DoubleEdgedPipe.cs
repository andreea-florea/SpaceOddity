using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class DoubleEdgedPipe
    {
        public EdgeType FirstEdge { get; set; }
        public EdgeType SecondEdge { get; set; }

        public DoubleEdgedPipe(EdgeType firstEdge, EdgeType secondEdge)
        {
            FirstEdge = firstEdge;
            SecondEdge = secondEdge;
        }
    }
}
