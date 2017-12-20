using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlueprintObserver
    {
        void BlockCreated(IBlueprint blueprint, Coordinate position);
        void BlockDeleted(IBlueprint blueprint, Coordinate position);
        void ShipComponentAdded(IBlueprint blueprint, Coordinate position);
        void ShipComponentDeleted(IBlueprint blueprint, Coordinate position);
        void DoubleEdgePipeAdded(IBlueprint blueprint, Coordinate position, DoubleEdgedPipe pipe);
        void DoubleEdgePipeDeleted(IBlueprint blueprint, Coordinate position, DoubleEdgedPipe pipe);
        void ConnectingPipeAdded(IBlueprint blueprint, Coordinate position, ConnectingPipe pipe);
        void ConnectingPipeDeleted(IBlueprint blueprint, Coordinate position, ConnectingPipe pipe);
    }
}
