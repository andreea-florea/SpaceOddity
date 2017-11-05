﻿using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Fancy;
using ViewModel.Fancy.Iternal;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class BlueprintBuilderFancyViewModelFactory
    {
        private IViewModelTilesFactory tilesFactory;
        private IWorldObjectFactory blockCoreFactory;
        private IWorldObjectFactory roundCornerFactory;
        private IWorldObjectFactory straightUpCornerFactory;
        private IWorldObjectFactory straightRightCornerFactory;
        private IWorldObjectFactory closedCornerFactory;
        private IWorldObjectFactory outsideUpCornerFactory;
        private IWorldObjectFactory outsideRightCornerFactory;
        private IWorldObjectFactory diagonalMissingCornerFactory;
        private IWorldObjectFactory roundEdgeFactory;
        private IWorldObjectFactory closedEdgeFactory;

        public BlueprintBuilderFancyViewModelFactory(
            IViewModelTilesFactory tilesFactory,
            IWorldObjectFactory blockCoreFactory,
            IWorldObjectFactory roundCornerFactory,
            IWorldObjectFactory straightUpCornerFactory,
            IWorldObjectFactory straightRightCornerFactory,
            IWorldObjectFactory closedCornerFactory,
            IWorldObjectFactory outsideUpCornerFactory,
            IWorldObjectFactory outsideRightCornerFactory,
            IWorldObjectFactory diagonalMissingCornerFactory,
            IWorldObjectFactory roundEdgeFactory,
            IWorldObjectFactory closedEdgeFactory)
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

        public BlueprintBuilderFancyViewModel CreateViewModel(IObservableBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var controller = new BlueprintBuilderController(builder);
            var controlAssigner = new BlueprintBuilderControlAssigner(controller);
            var tiles = tilesFactory.CreateTiles(controlAssigner, builder.Dimensions, fittingRectangle);

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

        private IDetailsViewUpdater CreateCornerUpdater(IObservableBlueprintBuilder builder,
            IBlueprintBuilderControlAssigner controlAssigner,
            IWorldObject[,] tiles, Coordinate direction)
        {
            var cornerDetails = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];

            var cornerUpdates = new List<FacingPosition>();

            cornerUpdates.Add(new FacingPosition(direction, Coordinates.Zero));
            cornerUpdates.Add(new FacingPosition(direction, -direction));
            cornerUpdates.Add(new FacingPosition(direction, -direction.RotateQuarterCircleRight()));
            cornerUpdates.Add(new FacingPosition(direction, -direction - direction.RotateQuarterCircleRight()));

            var baseCornerFactories = new IWorldObjectFactory[8];
            baseCornerFactories[0] = roundCornerFactory;
            baseCornerFactories[1] = straightUpCornerFactory;
            baseCornerFactories[2] = straightRightCornerFactory;
            baseCornerFactories[3] = diagonalMissingCornerFactory;
            baseCornerFactories[4] = roundCornerFactory;
            baseCornerFactories[5] = outsideUpCornerFactory;
            baseCornerFactories[6] = outsideRightCornerFactory;
            baseCornerFactories[7] = closedCornerFactory;
            var cornerFactory = new WorldObjectBitNumberFactoryPicker(baseCornerFactories,
                new CornerBlocksNumberGenerator(builder));
            
            return new BlockDetailsViewUpdater(
                builder, tiles, cornerDetails, cornerFactory, controlAssigner, cornerUpdates);
        }

        private IDetailsViewUpdater CreateEdgeUpdater(IObservableBlueprintBuilder builder,
            IBlueprintBuilderControlAssigner controlAssigner,
            IWorldObject[,] tiles, Coordinate direction)
        {
            var edgeDetails = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];

            var edgeUpdates = new List<FacingPosition>();

            edgeUpdates.Add(new FacingPosition(direction, Coordinates.Zero));
            edgeUpdates.Add(new FacingPosition(direction, -direction));

            var baseEdgeFactories = new IWorldObjectFactory[2];
            baseEdgeFactories[0] = roundEdgeFactory;
            baseEdgeFactories[1] = closedEdgeFactory;
            var edgeFactory = new WorldObjectBitNumberFactoryPicker(baseEdgeFactories,
                new EdgeBlocksNumberGenerator(builder));

            return new BlockDetailsViewUpdater(builder, tiles, edgeDetails, edgeFactory, controlAssigner, edgeUpdates);
        }

        private IDetailsViewUpdater CreateCoreUpdater(IObservableBlueprintBuilder builder,
            IBlueprintBuilderControlAssigner controlAssigner,
            IWorldObject[,] tiles)
        {
            var coreDetails = new IWorldObject[builder.Dimensions.Y, builder.Dimensions.X];

            var coreUpdates = new List<FacingPosition>();
            coreUpdates.Add(new FacingPosition(Coordinates.Zero, Coordinates.Zero));

            return new BlockDetailsViewUpdater(builder, tiles, coreDetails,
                new IgnoreFacingContextWorldObjectFactory(blockCoreFactory), controlAssigner, coreUpdates);
        }
    }
}
