using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public struct ConnectingPipe
    {
        public EdgeType Edge { get; set; }

        public ConnectingPipe(EdgeType edge) : this()
        {
            Edge = edge;
        }

        public bool IsEqualTo(ConnectingPipe pipe)
        {
            return Edge == pipe.Edge;
        }
    }
}
