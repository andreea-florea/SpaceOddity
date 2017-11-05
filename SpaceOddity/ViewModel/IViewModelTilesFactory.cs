using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Interfaces;

namespace ViewModel
{
    public interface IViewModelTilesFactory
    {
        IWorldObject[,] CreateTiles(IBlueprintBuilderControlAssigner controller, Coordinate dimensions, IRectangleSection fittingRectangle);
    }
}
