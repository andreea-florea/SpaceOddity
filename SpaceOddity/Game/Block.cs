using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Block : IBlock
    {
        public double Weight { get; private set; }

        public Block(double weight)
        {
            Weight = weight;
        }
    }
}
