using Game;
using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel;

namespace ConstructedGame
{
    public class GameViewFactory
    {
        public void CreateBlueprintBuilderView(IWorldObjectFactory tileObjectFactory,
            IWorldObjectFactory blockObjectFactory, IWorldObjectFactory shipComponentsFactory, 
            IRectangleSection fullRectangle)
        {
            var observableBlueprintBuilder = CreateBlueprintBuilder();
            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder, fullRectangle);
            var controller = new BlueprintBuilderController(observableBlueprintBuilder);
            var assignController = new BlueprintBuilderControlAssigner(controller);

            var tilesFactory = new ViewModelTilesFactory(tileObjectFactory);
            var blueprintViewModelFactory =
                new BlueprintBuilderViewModelFactory(tilesFactory, blockObjectFactory, shipComponentsFactory);
            blueprintViewModelFactory.CreateViewModel(observableBlueprintBuilder, fittingRectangle);
        }

        public void CreateBlueprintBuilderView(IWorldObjectFactory tileObjectFactory,
            IWorldObjectFactory blockCoreFactory,
            IWorldObjectFactory roundCornerFactory,
            IWorldObjectFactory straightUpCornerFactory,
            IWorldObjectFactory straightRightCornerFactory,
            IWorldObjectFactory closedCornerFactory,
            IWorldObjectFactory outsideUpCornerFactory,
            IWorldObjectFactory outsideRightCornerFactory,
            IWorldObjectFactory diagonalMissingCornerFactory,
            IWorldObjectFactory roundEdgeFactory,
            IWorldObjectFactory closedEdgeFactory,
            IRectangleSection fullRectangle)
        {
            var observableBlueprintBuilder = CreateBlueprintBuilder();
            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder, fullRectangle);

            var tilesFactory = new ViewModelTilesFactory(tileObjectFactory);
            var blueprintViewModelFactory =
                new BlueprintBuilderFancyViewModelFactory(tilesFactory,
                    blockCoreFactory,
                    roundCornerFactory,
                    straightUpCornerFactory,
                    straightRightCornerFactory,
                    closedCornerFactory,
                    outsideUpCornerFactory,
                    outsideRightCornerFactory,
                    diagonalMissingCornerFactory,
                    roundEdgeFactory,
                    closedEdgeFactory);
            blueprintViewModelFactory.CreateViewModel(observableBlueprintBuilder, fittingRectangle);
        }

        private IObservableBlueprintBuilder CreateBlueprintBuilder()
        {
            var blueprintBuilder = new BlueprintBuilder(new Coordinate(11, 11));
            return new ObservableBlueprintBuilder(blueprintBuilder);
        }

        private AspectRatioRectangleSection CreateViewRectangle(
            IObservableBlueprintBuilder observableBlueprintBuilder, IRectangleSection containingRectangle)
        {
            return new AspectRatioRectangleSection(
                new Vector2(observableBlueprintBuilder.Dimensions.X, observableBlueprintBuilder.Dimensions.Y),
                containingRectangle);
        }
    }
}
