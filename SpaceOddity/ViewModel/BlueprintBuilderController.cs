using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Actions;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class BlueprintBuilderController : IBlueprintBuilderController
    {
        public void AssignTileControl(IBlueprintBuilder blueprintBuilder, IWorldObject tile, int x, int y)
        {
            tile.LeftClickAction = new CreateBlockAction(blueprintBuilder, x, y);
        }

        public void AssignBlockControl(IBlueprintBuilder blueprintBuilder, IWorldObject tile, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
