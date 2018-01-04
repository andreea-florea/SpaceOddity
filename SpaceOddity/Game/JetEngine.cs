using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class JetEngine : IShipComponent, IBlockRestrictor
    {
        public IBlock Block { get; set; }


        public JetEngine()
        {

        }

        public bool CanCreateBlock(IBlueprint blueprint, Coordinate position)
        {
            return true;
        }
    }
}
