using Game.Interfaces;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ViewInterface;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class BlueprintBuilderViewModelFactory
    {
        private IWorldObjectFactory tileFactory;
        private IWorldObjectFactory blockFactory;
        private IBlueprintBuilderController controller;

        public BlueprintBuilderViewModelFactory(IWorldObjectFactory tileFactory, 
            IWorldObjectFactory blockFactory,
            IBlueprintBuilderController controller)
        {
            this.tileFactory = tileFactory;
            this.blockFactory = blockFactory;
            this.controller = controller;
        }

        public BlueprintBuilderViewModel CreateViewModel(IObservableBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var tiles = new IWorldObject[builder.Height, builder.Width];
            var blocks = new IWorldObject[builder.Height, builder.Width];
            var tileRects = fittingRectangle.Section.Split(builder.Width, builder.Height);

            var size = new Vector2(builder.Width, builder.Height);
            var scale = fittingRectangle.Section.Dimensions.Divide(size);

            for (var i = 0; i < builder.Height; ++i)
            {
                for (var j = 0; j < builder.Width; ++j)
                {
                    tiles[i, j] = tileFactory.CreateObject();
                    tiles[i, j].Position = tileRects[i, j].Center;
                    tiles[i, j].Scale = scale;
                    controller.AssignTileControl(builder, tiles[i, j], j, i);
                }
            }

            var viewModel = new BlueprintBuilderViewModel(tiles, blocks, blockFactory);
            builder.AttachObserver(viewModel);
            return viewModel;
        }
    }
}
