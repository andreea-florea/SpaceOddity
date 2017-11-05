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
    public class ViewModelTilesFactory : IViewModelTilesFactory
    {
        private IWorldObjectFactory tileFactory;

        public ViewModelTilesFactory(IWorldObjectFactory tileFactory)
        {
            this.tileFactory = tileFactory;
        }

        public IWorldObject[,] CreateTiles(IBlueprintBuilderControlAssigner controller, Coordinate dimensions, IRectangleSection fittingRectangle)
        {
            var tiles = new IWorldObject[dimensions.Y, dimensions.X];
            var tileRects = fittingRectangle.Section.Split(dimensions);

            var coordinateRectangle = new CoordinateRectangle(Coordinates.Zero, dimensions);
            foreach (var coordinate in coordinateRectangle.Points)
            {
                tiles.Set(coordinate, tileFactory.CreateObject());
                tiles.Get(coordinate).Position = tileRects.Get(coordinate).Center;
                tiles.Get(coordinate).Scale = tileRects.Get(coordinate).Dimensions;
                controller.AssignTileControl(tiles.Get(coordinate), coordinate);
            }
            return tiles;
        }
    }
}
