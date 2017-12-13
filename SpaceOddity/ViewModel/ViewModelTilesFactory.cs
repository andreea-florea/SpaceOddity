using Algorithm;
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
    public class ViewModelTilesFactory : IViewModelTilesFactory
    {
        private IFactory<IBuilderWorldObject> tileFactory;

        public ViewModelTilesFactory(IFactory<IBuilderWorldObject> tileFactory)
        {
            this.tileFactory = tileFactory;
        }

        public IBuilderWorldObject[,] CreateTiles(Coordinate dimensions, IRectangleSection fittingRectangle)
        {
            var tiles = new IBuilderWorldObject[dimensions.Y, dimensions.X];
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
