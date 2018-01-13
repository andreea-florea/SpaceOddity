using Algorithm;
using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.Controller;
using BlueprintBuildingViewModel.DataStructures;
using BlueprintBuildingViewModel.Fancy.Iternal;
using ViewModel;

namespace BlueprintBuildingViewModel.Fancy
{
    public class BlueprintBuilderFancyViewModelFactory
    {
        private IViewModelTilesFactory tilesFactory;
        private IRenderableFactory blockCoreFactory;
        private IRenderableFactory roundCornerFactory;
        private IRenderableFactory straightUpCornerFactory;
        private IRenderableFactory straightRightCornerFactory;
        private IRenderableFactory closedCornerFactory;
        private IRenderableFactory outsideUpCornerFactory;
        private IRenderableFactory outsideRightCornerFactory;
        private IRenderableFactory diagonalMissingCornerFactory;
        private IRenderableFactory roundEdgeFactory;
        private IRenderableFactory closedEdgeFactory;

        public BlueprintBuilderFancyViewModelFactory(
            IViewModelTilesFactory tilesFactory,
            IRenderableFactory blockCoreFactory,
            IRenderableFactory roundCornerFactory,
            IRenderableFactory straightUpCornerFactory,
            IRenderableFactory straightRightCornerFactory,
            IRenderableFactory closedCornerFactory,
            IRenderableFactory outsideUpCornerFactory,
            IRenderableFactory outsideRightCornerFactory,
            IRenderableFactory diagonalMissingCornerFactory,
            IRenderableFactory roundEdgeFactory,
            IRenderableFactory closedEdgeFactory)
        {
            this.tilesFactory = tilesFactory;
            this.blockCoreFactory = blockCoreFactory;
            this.roundCornerFactory = roundCornerFactory;
            this.straightUpCornerFactory = straightUpCornerFactory;
            this.straightRightCornerFactory = straightRightCornerFactory;
            this.closedCornerFactory = closedCornerFactory;
            this.outsideUpCornerFactory = outsideUpCornerFactory;
            this.outsideRightCornerFactory = outsideRightCornerFactory;
            this.diagonalMissingCornerFactory = diagonalMissingCornerFactory;
            this.roundEdgeFactory = roundEdgeFactory;
            this.closedEdgeFactory = closedEdgeFactory;
        }

        public BlueprintBuilderFancyViewModel CreateViewModel(IBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var tiles = tilesFactory.CreateTiles(builder.Dimensions, fittingRectangle);
            var tableHighlighter = new BlueprintBuilderTableHighlighter(null);
            var controlAssigner = CreateController(builder, tableHighlighter);

            var detailsUpdaters = new List<IDetailsViewUpdater>();

            foreach (var direction in Coordinates.Directions)
            {
                detailsUpdaters.Add(CreateCornerUpdater(builder, controlAssigner, tiles, direction));
                detailsUpdaters.Add(CreateEdgeUpdater(builder, controlAssigner, tiles, direction));
            }
            detailsUpdaters.Add(CreateCoreUpdater(builder, controlAssigner, tiles));

            var viewModel = new BlueprintBuilderFancyViewModel(detailsUpdaters);
            builder.AttachObserver(viewModel);
            return viewModel;
        }

        private BlueprintBuilderControlAssigner CreateController(IBlueprintBuilder builder,
            IBlueprintBuilderTableHighlighter tableHighlighter)
        {
            var controllerFactory = new BlueprintBuilderControllerFactory();
            var controller = controllerFactory.CreateController(builder, tableHighlighter);
            var controlAssigner = new BlueprintBuilderControlAssigner(controller);
            return controlAssigner;
        }

        private IDetailsViewUpdater CreateCornerUpdater(IBlueprintBuilder builder,
            IBlueprintBuilderControlAssigner controlAssigner,
            IWorldObject[,] tiles, Coordinate direction)
        {
            var cornerDetails = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];

            var cornerUpdates = new List<FacingPosition>();

            cornerUpdates.Add(new FacingPosition(direction, Coordinates.Zero));
            cornerUpdates.Add(new FacingPosition(direction, -direction));
            cornerUpdates.Add(new FacingPosition(direction, -direction.RotateQuarterCircleRight()));
            cornerUpdates.Add(new FacingPosition(direction, -direction - direction.RotateQuarterCircleRight()));

            var baseCornerFactories = new IFactory<IWorldObject>[8];
            baseCornerFactories[0] = new WorldObjectFactory(roundCornerFactory);
            baseCornerFactories[1] = new WorldObjectFactory(straightUpCornerFactory);
            baseCornerFactories[2] = new WorldObjectFactory(straightRightCornerFactory);
            baseCornerFactories[3] = new WorldObjectFactory(diagonalMissingCornerFactory);
            baseCornerFactories[4] = new WorldObjectFactory(roundCornerFactory);
            baseCornerFactories[5] = new WorldObjectFactory(outsideUpCornerFactory);
            baseCornerFactories[6] = new WorldObjectFactory(outsideRightCornerFactory);
            baseCornerFactories[7] = new WorldObjectFactory(closedCornerFactory);
            var cornerFactory = new WorldObjectBitNumberFactoryPicker(baseCornerFactories,
                new CornerBlocksNumberGenerator(builder));
            
            return new BlockDetailsViewUpdater(
                builder, tiles, cornerDetails, cornerFactory, controlAssigner, cornerUpdates);
        }

        private IDetailsViewUpdater CreateEdgeUpdater(IBlueprintBuilder builder,
            IBlueprintBuilderControlAssigner controlAssigner,
            IWorldObject[,] tiles, Coordinate direction)
        {
            var edgeDetails = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];

            var edgeUpdates = new List<FacingPosition>();

            edgeUpdates.Add(new FacingPosition(direction, Coordinates.Zero));
            edgeUpdates.Add(new FacingPosition(direction, -direction));

            var baseEdgeFactories = new IFactory<IWorldObject>[2];
            baseEdgeFactories[0] = new WorldObjectFactory(roundEdgeFactory);
            baseEdgeFactories[1] = new WorldObjectFactory(closedEdgeFactory);
            var edgeFactory = new WorldObjectBitNumberFactoryPicker(baseEdgeFactories,
                new EdgeBlocksNumberGenerator(builder));

            return new BlockDetailsViewUpdater(builder, tiles, edgeDetails, edgeFactory, controlAssigner, edgeUpdates);
        }

        private IDetailsViewUpdater CreateCoreUpdater(IBlueprintBuilder builder,
            IBlueprintBuilderControlAssigner controlAssigner,
            IWorldObject[,] tiles)
        {
            var coreDetails = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];

            var coreUpdates = new List<FacingPosition>();
            coreUpdates.Add(new FacingPosition(Coordinates.Zero, Coordinates.Zero));

            return new BlockDetailsViewUpdater(builder, tiles, coreDetails,
                new IgnoreFacingContextWorldObjectFactory(new WorldObjectFactory(blockCoreFactory)),
                controlAssigner, coreUpdates);
        }
    }
}
