using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class BatteryFactory : IShipComponentFactory
    {
        public IShipComponent CreateComponent()
        {
            return new Battery();
        }
    }
}
