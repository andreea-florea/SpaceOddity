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
using ViewModel.MenuControls;
using Algorithms;
using Algorithm;

namespace ConstructedGame
{
    public class GameViewFactory
    {
        private Coordinate boardSize;

        public GameViewFactory()
        {
            this.boardSize = new Coordinate(11, 11);
        }

        public void CreateBlueprintBuilderView(
            IFactory<IRenderable> tileObjectFactory,
            IFactory<IRenderable> blockRenderableFactory, 
            IFactory<IRenderable> shipComponentsFactory, 
            IFactory<IRenderable> emptyComponentsFactory,
            IFactory<IRenderable> pipeLinkFactory,
            IFactory<IRenderable, ICurve> pipeFactory,
            IEnumerable<IFactory<IRenderable>> blockIconRenderableFactories,
            IRectangleSection fullRectangle)
        {
            var observableBlueprintBuilder = CreateBlueprintBuilder(boardSize);
            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder, fullRectangle);

            CreateUserInterface(blockIconRenderableFactories);

            var tilesFactory = new ViewModelTilesFactory(new ActivateableWorldObjectFactory(tileObjectFactory));
            var blueprintViewModelFactory = new ViewModelFactory(
                tilesFactory, 
                blockRenderableFactory, 
                shipComponentsFactory, 
                emptyComponentsFactory, 
                pipeLinkFactory, 
                pipeFactory);
            blueprintViewModelFactory.CreateViewModel(observableBlueprintBuilder, fittingRectangle);
        }

        private void CreateUserInterface(IEnumerable<IFactory<IRenderable>> blockIconRenderableFactories)
        {
            var dropDownFactory = new DropDownListFactory(
                blockIconRenderableFactories.Select(factory => (IFactory<IWorldObject>)new WorldObjectFactory(factory)).ToList(),
                new Vector2(30, 30), Vector2s.Up * 50, 0);
            dropDownFactory.Create();
        }

        public void CreateBlueprintBuilderView(
            IFactory<IRenderable> tileObjectFactory,
            IFactory<IRenderable> blockCoreFactory,
            IFactory<IRenderable> roundCornerFactory,
            IFactory<IRenderable> straightUpCornerFactory,
            IFactory<IRenderable> straightRightCornerFactory,
            IFactory<IRenderable> closedCornerFactory,
            IFactory<IRenderable> outsideUpCornerFactory,
            IFactory<IRenderable> outsideRightCornerFactory,
            IFactory<IRenderable> diagonalMissingCornerFactory,
            IFactory<IRenderable> roundEdgeFactory,
            IFactory<IRenderable> closedEdgeFactory,
            IRectangleSection fullRectangle)
        {
            var observableBlueprintBuilder = CreateBlueprintBuilder(boardSize);
            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder, fullRectangle);

            var tilesFactory = new ViewModelTilesFactory(new ActivateableWorldObjectFactory(tileObjectFactory));
            var blueprintViewModelFactory = new FancyViewModelFactory(
                tilesFactory,
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
                new BlockFactory(2.0),
                new BatteryFactory(),
                new EmptyShipComponentFactory());
        }

        private AspectRatioRectangleSection CreateViewRectangle(
            IBlueprintBuilder blueprintBuilder, IRectangleSection containingRectangle)
        {
            return new AspectRatioRectangleSection(
                blueprintBuilder.Dimensions.ToVector2(), containingRectangle);
        }
    }
}
