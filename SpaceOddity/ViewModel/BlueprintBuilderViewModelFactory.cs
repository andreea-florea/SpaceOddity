using Game.Interfaces;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel
{
    public class BlueprintBuilderViewModelFactory
    {
        private IViewModelTilesFactory tilesFactory;
        private IWorldObjectFactory blockFactory;

        public BlueprintBuilderViewModelFactory(IViewModelTilesFactory tilesFactory, 
            IWorldObjectFactory blockFactory)
        {
            this.tilesFactory = tilesFactory;
            this.blockFactory = blockFactory;
        }

        public BlueprintBuilderViewModel CreateViewModel(IObservableBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var controller = new BlueprintBuilderController(builder);
            var controlAssigner = new BlueprintBuilderControlAssigner(controller);
            var tiles = tilesFactory.CreateTiles(controlAssigner, builder.Dimensions, fittingRectangle);
            var blocks = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];
            var viewModel = new BlueprintBuilderViewModel(tiles, blocks, blockFactory, controlAssigner);
            builder.AttachObserver(viewModel);
            return viewModel;
        }
    }
}
