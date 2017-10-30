using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlueprintBuilder
    {
        int Width { get; }
        int Height { get; }
        IBlock GetBlock(Coordinate position);
        bool HasBlock(Coordinate position);
        bool CreateBlock(Coordinate position);
        bool DeleteBlock(Coordinate position);
        bool AddShipComponent(Coordinate position, IShipComponent component);
        bool DeleteShipComponent(Coordinate position);
    }
}
