using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlockRestrictor
    {
        bool CanCreateBlock(IBlueprint blueprint, Coordinate position); 
    }
}
