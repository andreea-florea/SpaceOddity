using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueprintBuildingViewModel.DataStructures;
using ViewModel;

namespace BlueprintBuildingViewModel
{
    public interface IBlueprintBuilderObjectTable
    {
        IActivateableWorldObject GetTile(Coordinate position);
        IActivateableWorldObject GetBlock(Coordinate position);
        void SetBlock(Coordinate position, IActivateableWorldObject block);
        IActivateableWorldObject GetShipComponent(Coordinate position);
        void SetShipComponent(Coordinate position, IActivateableWorldObject shipComponent);
        IActivateableWorldObject GetPipeLink(CoordinatePair edge);
        void SetPipeLink(CoordinatePair edge, IActivateableWorldObject pipeLink);
        void DeletePipeLink(CoordinatePair edge);
        void DeleteBlock(Coordinate position);
    }
}
