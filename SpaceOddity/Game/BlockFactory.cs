using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms;
using Game.Interfaces;

namespace Game
{
    public class BlockFactory : IFactory<IBlock>
    {
        private double weight;

        public BlockFactory(double weight)
        {
            this.weight = weight;
        }

        public IBlock Create()
        {
            return new Block(weight, new List<DoubleEdgedPipe>(), new List<ConnectingPipe>());
        }
    }
}
