using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Interfaces
{
    public interface IBlueprintBuilder
    {
        int Width { get; }
        int Height { get; }
        IBlock GetBlock(int y, int x);
        bool CreateBlock(int y, int x);
        bool DeleteBlock(int y, int x);
        bool AddShipComponent(int y, int x, IShipComponent component);
        bool DeleteShipComponent(int y, int x);
    }
}
