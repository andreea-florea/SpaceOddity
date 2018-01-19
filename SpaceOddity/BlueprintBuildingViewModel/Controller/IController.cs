using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueprintBuildingViewModel.DataStructures;

namespace BlueprintBuildingViewModel.Controller
{
    public interface IController
    {
        void SelectTile(Coordinate position);
        void SelectBlock(Coordinate position);
        void CancelBlock(Coordinate position);
        void SelectShipComponent(Coordinate position);
        void CancelShipComponent(Coordinate position);
        void SelectPipeLink(CoordinatePair edge);
    }
}
