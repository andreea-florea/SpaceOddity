using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlueprintObserver
    {
        void BlockCreated(IBlueprint blueprintBuilder, Coordinate position);
        void BlockDeleted(IBlueprint blueprintBuilder, Coordinate position);
        void ShipComponentAdded(IBlueprint blueprintBuilder, Coordinate position);
        void ShipComponentDeleted(IBlueprint blueprintBuilder, Coordinate position);
        void DoubleEdgePipeAdded(IBlueprint blueprintBuilder, Coordinate position);
        void DoubleEdgePipeDeleted(IBlueprint blueprintBuilder, Coordinate position);
        void ConnectingPipeAdded(IBlueprint blueprintBuilder, Coordinate position);
        void ConnectingPipeDeleted(IBlueprint blueprintBuilder, Coordinate position);
    }
}
