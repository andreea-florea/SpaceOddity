using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.Interfaces
{
    public interface IBlueprintBuilderController
    {
        void AssignTileControl(IBlueprintBuilder blueprintBuilder, IWorldObject tile, Coordinate position);
        void AssignBlockControl(IBlueprintBuilder blueprintBuilder, IWorldObject tile, Coordinate position);
    }
}
