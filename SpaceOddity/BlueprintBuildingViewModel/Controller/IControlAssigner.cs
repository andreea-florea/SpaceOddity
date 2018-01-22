using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.DataStructures;
using ViewModel;

namespace BlueprintBuildingViewModel.Controller
{
    public interface IControlAssigner
    {
        void AssignTileControl(IWorldObject tile, Coordinate position);
        void AssignBlockControl(IWorldObject block, Coordinate position);
        void AssignPipeLinkControl(IWorldObject pipeLink, CoordinatePair edge);
        void AssignShipComponentControl(IWorldObject shipComponent, Coordinate position);
    }
}
