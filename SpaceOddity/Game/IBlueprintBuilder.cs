using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IBlueprintBuilder
    {
        int Width { get; }
        int Height { get; }
        IBlock GetBlock(int y, int x);
        bool CreateBlock(int y, int x);
    }
}
