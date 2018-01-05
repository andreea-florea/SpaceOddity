using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Game.Interfaces
{
    public interface IShipComponent
    {
        void AdditionalSetups(IBlueprintBuilder blueprintBuilder);
        void RemoveAdditionalSetups(IBlueprintBuilder blueprintBuilder);
    }
}
