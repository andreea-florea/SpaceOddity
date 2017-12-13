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
using ViewModel.Controller;

namespace ConstructedGame
{
    public class GameViewFactory
    {
        public void CreateBlueprintBuilderView(IRenderableFactory tileObjectFactory,
            IRenderableFactory blockRenderableFactory, 
            IRenderableFactory shipComponentsFactory, 
            IRenderableFactory pipeLinkFactory,
            IRectangleSection fullRectangle)
        {
            var observableBlueprintBuilder = CreateBlueprintBuilder();
            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder, fullRectangle);

            var tilesFactory = new ViewModelTilesFactory(new BuilderWorldObjectFactory(tileObjectFactory));
            var blueprintViewModelFactory = new BlueprintBuilderViewModelFactory(
                tilesFactory, blockRenderableFactory, shipComponentsFactory, pipeLinkFactory);
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
            var observableBlueprintBuilder = CreateBlueprintBuilder();
            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder, fullRectangle);

            var tilesFactory = new ViewModelTilesFactory(new BuilderWorldObjectFactory(tileObjectFactory));
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

        private IBlueprintBuilder CreateBlueprintBuilder()
        {
            return new BlueprintBuilder(new Coordinate(11, 11));
        }

        private AspectRatioRectangleSection CreateViewRectangle(
            IBlueprintBuilder blueprintBuilder, IRectangleSection containingRectangle)
        {
            return new AspectRatioRectangleSection(
                blueprintBuilder.Dimensions.ToVector2(), containingRectangle);
        }
    }
}
