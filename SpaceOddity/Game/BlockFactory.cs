using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Interfaces;

namespace Game
{
    public class BlockFactory : IBlockFactory
    {
        private double weight;

        public BlockFactory(double weight)
        {
            this.weight = weight;
        }

        public IBlock CreateBlock()
        {
            return new Block(weight, new List<DoubleEdgedPipe>(), new List<ConnectingPipe>());
        }
    }
}
