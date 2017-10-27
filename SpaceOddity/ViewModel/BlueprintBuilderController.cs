using Game.Interfaces;
using NaturalNumbersMath;
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
        public void AssignTileControl(IBlueprintBuilder blueprintBuilder, IWorldObject tile, Coordinate position)
        {
            tile.LeftClickAction = new CreateBlockAction(blueprintBuilder, position);
        }

        public void AssignBlockControl(IBlueprintBuilder blueprintBuilder, IWorldObject tile, Coordinate position)
        {
            throw new NotImplementedException();
        }
    }
}
