using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Game.Interfaces
{
    public interface IBlueprintBuilderObserver
    {
        void BlockCreated(IBlueprintBuilder blueprintBuilder, Coordinate position);
        void ErrorBlockNotCreated(IBlueprintBuilder blueprintBuilder, Coordinate position);

        void BlockDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position);
        void ErrorBlockNotDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position);

        void ShipComponentAdded(IBlueprintBuilder blueprintBuilder, Coordinate position);
        void ErrorShipComponentNotAdded(IBlueprintBuilder blueprintBuilder, Coordinate position);

        void ShipComponentDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position);
        void ErrorShipComponentNotDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position);
    }
}
