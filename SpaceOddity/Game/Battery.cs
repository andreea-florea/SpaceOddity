using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Interfaces;
using Game.Enums;

namespace Game
{
    public class Battery : IShipComponent
    {
        public BlueprintShipComponentType Type
        {
            get
            {
                return BlueprintShipComponentType.Battery;
            }
        }

        public void AdditionalSetups(IBlueprintBuilder blueprintBuilder)
        {
            return;
        }

        public void RemoveAdditionalSetups(IBlueprintBuilder blueprintBuilder)
        {
            return;
        }
    }
}
