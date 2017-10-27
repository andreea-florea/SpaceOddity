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

            for (var y = 0; y < builder.Height; ++y)
            {
                for (var x = 0; x < builder.Width; ++x)
                {
                    tiles[y, x] = tileFactory.CreateObject();
                    tiles[y, x].Position = tileRects[y, x].Center;
                    tiles[y, x].Scale = tileRects[y, x].Dimensions;
                    controller.AssignTileControl(builder, tiles[y, x], new Coordinate(x, y));
                }
            }
            return tiles;
        }
    }
}
