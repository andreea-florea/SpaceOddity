using Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Game.Interfaces
{
    public interface IShipComponent
    {
        BlueprintShipComponentType Type { get; }
        void AdditionalSetups(IBlueprintBuilder blueprintBuilder);
        void RemoveAdditionalSetups(IBlueprintBuilder blueprintBuilder);
    }
}
