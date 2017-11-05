using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.Interfaces
{
    public interface IBlueprintBuilderControlAssigner
    {
        void AssignTileControl(IWorldObject tile, Coordinate position);
        void AssignBlockControl(IWorldObject block, Coordinate position);
    }
}
