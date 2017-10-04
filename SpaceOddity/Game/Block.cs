using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Interfaces;

namespace Game
{
    public class Block : IBlock
    {
        public double Weight { get; private set; }

        public IShipComponent ShipComponent { get; private set; }

        public Block(double weight)
        {
            Weight = weight;
        }

        public bool AddShipComponent(IShipComponent component)
        {
            return false;
        }
    }
}
