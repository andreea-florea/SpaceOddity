using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Controller;

namespace ViewModel
{
    public interface IViewModelTilesFactory
    {
        IBuilderWorldObject[,] CreateTiles(Coordinate dimensions, IRectangleSection fittingRectangle);
    }
}
