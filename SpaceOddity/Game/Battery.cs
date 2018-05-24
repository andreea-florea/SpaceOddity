using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Interfaces;
using Game.Enums;
using NaturalNumbersMath;

namespace Game
{
    public class Battery : IShipComponent
    {
        public void AdditionalSetups(IBlueprintBuilder blueprintBuilder)
        {
            return;
        }

        public void RemoveAdditionalSetups(IBlueprintBuilder blueprintBuilder)
        {
            return;
        }

        public bool CanBePlaced(IBlueprint blueprint, Coordinate position)
        {
            return true;
        }
    }
}
