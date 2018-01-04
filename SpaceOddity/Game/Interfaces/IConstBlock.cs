using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IConstBlock
    {
        IEnumerable<DoubleEdgedPipe> PipesWithBothEdges { get; }
        IEnumerable<ConnectingPipe> PipesWithOneEdge { get; }

        double Weight { get; }
        Coordinate Position { get; }
        bool HasShipComponent();
        void SetPosition(Coordinate position);
    }
}
