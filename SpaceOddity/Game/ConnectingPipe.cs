using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Enums;

namespace Game
{
    public class ConnectingPipe
    {
        public EdgeType Edge { get; set; }

        public ConnectingPipe(EdgeType edge)
        {
            Edge = edge;
        }

        public bool IsEqualTo(ConnectingPipe pipe)
        {
            return Edge == pipe.Edge;
        }
    }
}
