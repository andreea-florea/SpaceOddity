using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewInterface;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class BlueprintBuilderController : IBlueprintBuilderController
    {
        public void AssignTileControl(IBlueprintBuilder blueprintBuilder, IWorldObject tile, int x, int y)
        {
            tile.LeftClickAction = new CreateBlockAction(blueprintBuilder, x, y);
        }
    }
}
