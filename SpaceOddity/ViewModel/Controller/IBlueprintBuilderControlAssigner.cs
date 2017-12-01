using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.DataStructures;

namespace ViewModel.Controller
{
    public interface IBlueprintBuilderControlAssigner
    {
        void AssignTileControl(IWorldObject tile, Coordinate position);
        void AssignBlockControl(IWorldObject block, Coordinate position);
        void AssignPipeLinkControl(IWorldObject pipeLink, CoordinatePair edge);
    }
}
