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
        private IRenderableFactory blockFactory;
        private IRenderableFactory shipComponentsFactory;

        public BlueprintBuilderViewModelFactory(IViewModelTilesFactory tilesFactory,
            IRenderableFactory blockFactory, IRenderableFactory shipComponentsFactory)
        {
            this.tilesFactory = tilesFactory;
            this.blockFactory = blockFactory;
            this.shipComponentsFactory = shipComponentsFactory;
        }

        public BlueprintBuilderViewModel CreateViewModel(IObservableBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var controller = new BlueprintBuilderController(builder);
            var controlAssigner = new BlueprintBuilderControlAssigner(controller);
            var tiles = tilesFactory.CreateTiles(controlAssigner, builder.Dimensions, fittingRectangle);
            var blocks = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];
            var shipComponents = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];
            var viewModel = new BlueprintBuilderViewModel(tiles, blocks, shipComponents, 
                new WorldObjectFactory(blockFactory), 
                new WorldObjectFactory(shipComponentsFactory), controlAssigner);
            builder.AttachObserver(viewModel);
            return viewModel;
        }
    }
}
