using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewInterface;

namespace ViewModel.Interfaces
{
    public interface IBlueprintBuilderController
    {
        void AssignTileControl(IBlueprintBuilder blueprintBuilder, IWorldObject tile, int x, int y);
    }
}
