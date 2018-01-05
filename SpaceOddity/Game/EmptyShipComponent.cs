using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Interfaces;

namespace Game
{
    public class EmptyShipComponent : IShipComponent
    {
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
