using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Interfaces;

namespace Game
{
    public class BlockFactory : IBlockFactory
    {
        private double weight;
        private IShipComponent shipComponent;

        public BlockFactory(double weight, IShipComponent shipComponent)
        {
            this.weight = weight;
            this.shipComponent = shipComponent;
        }

        public IBlock CreateBlock()
        {
            return new Block(weight, shipComponent);
        }
    }
}
