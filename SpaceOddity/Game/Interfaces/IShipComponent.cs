using Game.Enums;
using NaturalNumbersMath;
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
        bool CanBePlaced(IBlueprint blueprint, Coordinate position);
    }
}
