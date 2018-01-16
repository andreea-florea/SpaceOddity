using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Interfaces;
using Game.Enums;

namespace Game
{
    public class EmptyShipComponent : IShipComponent
    {
        public BlueprintShipComponentType Type 
        { 
            get
            {
                return BlueprintShipComponentType.Empty;
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
