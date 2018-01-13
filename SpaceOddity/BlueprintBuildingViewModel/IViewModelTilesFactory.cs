using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.Controller;
using ViewModel;

namespace BlueprintBuildingViewModel
{
    public interface IViewModelTilesFactory
    {
        IActivateableWorldObject[,] CreateTiles(Coordinate dimensions, IRectangleSection fittingRectangle);
    }
}
