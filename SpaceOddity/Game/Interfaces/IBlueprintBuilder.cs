using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlueprintBuilder
    {
        Coordinate Dimensions { get; }
        IBlock GetBlock(Coordinate position);
        bool HasBlock(Coordinate position);
        bool CreateBlock(Coordinate position);
        bool DeleteBlock(Coordinate position);
        bool AddShipComponent(Coordinate position);
        bool DeleteShipComponent(Coordinate position);
    }
}
