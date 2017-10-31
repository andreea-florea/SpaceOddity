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
        private IViewModelTilesFactory tilesFactory;
        private IWorldObjectFactory blockFactory;
        private IBlueprintBuilderController controller;

        public BlueprintBuilderViewModelFactory(IViewModelTilesFactory tilesFactory, 
            IWorldObjectFactory blockFactory,
            IBlueprintBuilderController controller)
        {
            this.tilesFactory = tilesFactory;
            this.blockFactory = blockFactory;
            this.controller = controller;
        }

        public BlueprintBuilderViewModel CreateViewModel(IObservableBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var tiles = tilesFactory.CreateTiles(builder, fittingRectangle);
            var blocks = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];
            var viewModel = new BlueprintBuilderViewModel(tiles, blocks, blockFactory, controller);
            builder.AttachObserver(viewModel);
            return viewModel;
        }
    }
}
