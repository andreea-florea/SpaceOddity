using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueprintBuildingViewModel.DataStructures;

namespace BlueprintBuildingViewModel.Controller
{
    public interface IBlueprintBuilderController
    {
        void TileSelect(Coordinate position);
        void BlockSelect(Coordinate position);
        void BlockCancel(Coordinate position);
        void ShipComponentSelect(Coordinate position);
        void ShipComponentCancel(Coordinate position);
        void PipeLinkSelect(CoordinatePair edge);
    }
}
