using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Game.Interfaces
{
    public interface IBlueprintBuilderObserver
    {
        void BlockCreated(IBlueprintBuilder blueprintBuilder, int y, int x);
        void ErrorBlockNotCreated(IBlueprintBuilder blueprintBuilder, int y, int x);

        void BlockDeleted(IBlueprintBuilder blueprintBuilder, int y, int x);
        void ErrorBlockNotDeleted(IBlueprintBuilder blueprintBuilder, int y, int x);

        void ShipComponentAdded(IBlueprintBuilder blueprintBuilder, int y, int x);
        void ErrorShipComponentNotAdded(IBlueprintBuilder blueprintBuilder, int y, int x);

        void ShipComponentDeleted(IBlueprintBuilder blueprintBuilder, int y, int x);
        void ErrorShipComponentNotDeleted(IBlueprintBuilder blueprintBuilder, int y, int x);
    }
}
