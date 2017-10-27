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
        private IBlueprintBuilderController controller;

        public ViewModelTilesFactory(IWorldObjectFactory tileFactory, IBlueprintBuilderController controller)
        {
            this.tileFactory = tileFactory;
            this.controller = controller;
        }

        public IWorldObject[,] CreateTiles(IObservableBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var tiles = new IWorldObject[builder.Height, builder.Width];
            var tileRects = fittingRectangle.Section.Split(builder.Width, builder.Height);

            for (var line = Coordinates.Zero; line.Y < builder.Height; line += Coordinates.Up)
            {
                for (var coordinate = line; coordinate.X < builder.Width; coordinate += Coordinates.Right)
                {
                    tiles[coordinate.Y, coordinate.X] = tileFactory.CreateObject();
                    tiles.Get(coordinate).Position = tileRects.Get(coordinate).Center;
                    tiles.Get(coordinate).Scale = tileRects.Get(coordinate).Dimensions;
                    controller.AssignTileControl(builder, tiles.Get(coordinate), coordinate);
                }

            }
            return tiles;
        }
    }
}
