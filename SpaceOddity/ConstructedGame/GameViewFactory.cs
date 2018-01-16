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
using BlueprintBuildingViewModel.Controller;
using BlueprintBuildingViewModel.Fancy;
using BlueprintBuildingViewModel;

namespace ConstructedGame
{
    public class GameViewFactory
    {
        public void CreateBlueprintBuilderView(IRenderableFactory tileObjectFactory,
            IRenderableFactory blockRenderableFactory, 
            IRenderableFactory shipComponentsFactory, 
            IRenderableFactory emptyComponentsFactory,
            IRenderableFactory pipeLinkFactory,
            ICurveRenderableFactory pipeFactory,
            IRectangleSection fullRectangle)
        {
            var observableBlueprintBuilder = CreateBlueprintBuilder(new Coordinate(11, 11));
            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder, fullRectangle);

            var tilesFactory = new ViewModelTilesFactory(new ActivateableWorldObjectFactory(tileObjectFactory));
            var blueprintViewModelFactory = new BlueprintBuilderViewModelFactory(
                tilesFactory, blockRenderableFactory, shipComponentsFactory, emptyComponentsFactory, pipeLinkFactory, pipeFactory);
            blueprintViewModelFactory.CreateViewModel(observableBlueprintBuilder, fittingRectangle);
        }

        public void CreateBlueprintBuilderView(IRenderableFactory tileObjectFactory,
            IRenderableFactory blockCoreFactory,
            IRenderableFactory roundCornerFactory,
            IRenderableFactory straightUpCornerFactory,
            IRenderableFactory straightRightCornerFactory,
            IRenderableFactory closedCornerFactory,
            IRenderableFactory outsideUpCornerFactory,
            IRenderableFactory outsideRightCornerFactory,
            IRenderableFactory diagonalMissingCornerFactory,
            IRenderableFactory roundEdgeFactory,
            IRenderableFactory closedEdgeFactory,
            IRectangleSection fullRectangle)
        {
            var observableBlueprintBuilder = CreateBlueprintBuilder(new Coordinate(11, 11));
            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder, fullRectangle);

            var tilesFactory = new ViewModelTilesFactory(new ActivateableWorldObjectFactory(tileObjectFactory));
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

        private IBlueprintBuilder CreateBlueprintBuilder(Coordinate size)
        {
            return new BlueprintBuilder(
                new Blueprint(new IBlock[size.Y, size.X]),
                new BlockFactory(1.0), new BatteryFactory(), new EmptyShipComponentFactory());
        }

        private AspectRatioRectangleSection CreateViewRectangle(
            IBlueprintBuilder blueprintBuilder, IRectangleSection containingRectangle)
        {
            return new AspectRatioRectangleSection(
                blueprintBuilder.Dimensions.ToVector2(), containingRectangle);
        }
    }
}
