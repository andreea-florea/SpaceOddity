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
        private IBlueprintBuilderControlAssigner controller;

        public ViewModelTilesFactory(IWorldObjectFactory tileFactory, IBlueprintBuilderControlAssigner controller)
        {
            this.tileFactory = tileFactory;
            this.controller = controller;
        }

        public IWorldObject[,] CreateTiles(IObservableBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var tiles = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];
            var tileRects = fittingRectangle.Section.Split(builder.Dimensions);

            var coordinateRectangle = new CoordinateRectangle(Coordinates.Zero, builder.Dimensions);
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
