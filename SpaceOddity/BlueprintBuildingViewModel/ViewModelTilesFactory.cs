using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.Controller;
using ViewModel;
using Algorithms;

namespace BlueprintBuildingViewModel
{
    public class ViewModelTilesFactory : IViewModelTilesFactory
    {
        private IFactory<IActivateableWorldObject> tileFactory;

        public ViewModelTilesFactory(IFactory<IActivateableWorldObject> tileFactory)
        {
            this.tileFactory = tileFactory;
        }

        public IActivateableWorldObject[,] CreateTiles(Coordinate dimensions, IRectangleSection fittingRectangle)
        {
            var tiles = new IActivateableWorldObject[dimensions.Y, dimensions.X];
            var tileRects = fittingRectangle.Section.Split(dimensions);

            var coordinateRectangle = new CoordinateRectangle(Coordinates.Zero, dimensions);
            foreach (var coordinate in coordinateRectangle.Points)
            {
                tiles.Set(coordinate, tileFactory.Create());
                tiles.Get(coordinate).Position = tileRects.Get(coordinate).Center;
                tiles.Get(coordinate).Scale = tileRects.Get(coordinate).Dimensions;
            }
            return tiles;
        }
    }
}
