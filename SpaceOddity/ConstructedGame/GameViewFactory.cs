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
using ViewModel.ModelDetailsConnection;
using Game.Enums;

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
            var shipComponentDetails = new Details<IShipComponent>();
            var blockDetails = new Details<IBlock>();
            var observableBlueprintBuilder = CreateBlueprintBuilder(boardSize, shipComponentDetails, blockDetails);
            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder, fullRectangle);

            CreateUserInterface(blockIconRenderableFactories);

            var tilesFactory = new ViewModelTilesFactory(new ActivateableWorldObjectFactory(tileObjectFactory));
            var blueprintViewModelFactory = new ViewModelFactory(
                tilesFactory, 
                blockRenderableFactory, 
                shipComponentsFactory, 
                emptyComponentsFactory, 
                pipeLinkFactory, 
                pipeFactory,
                shipComponentDetails);
            blueprintViewModelFactory.CreateViewModel(observableBlueprintBuilder, fittingRectangle);
        }

        private void CreateUserInterface(IEnumerable<IFactory<IRenderable>> blockIconRenderableFactories)
        {
            var dropDownFactory = new DropDownListFactory(
                blockIconRenderableFactories.Select(factory => (IFactory<IWorldObject>)new WorldObjectFactory(factory)).ToList(),
                new Vector2(30, 30), Vector2s.Up * 50, 0);
            dropDownFactory.Create();
        }

        private IBlueprintBuilder CreateBlueprintBuilder(
            Coordinate size,
            Details<IShipComponent> shipComponentDetails,
            Details<IBlock> blockComponentDetails)
        {
            return new BlueprintBuilder(
                new Blueprint(new IBlock[size.Y, size.X]),
                new List<IFactory<IBlock>> { new BlockFactory(2.0) },
                new RegisterDetailFactory<IShipComponent, IConstBlock>(new JetEngineFactory(EdgeType.DOWN), shipComponentDetails, 1),
                new RegisterDetailFactory<IShipComponent, IConstBlock>(new EmptyShipComponentFactory(), shipComponentDetails, 0));
        }

        private AspectRatioRectangleSection CreateViewRectangle(
            IBlueprintBuilder blueprintBuilder, IRectangleSection containingRectangle)
        {
            return new AspectRatioRectangleSection(
                blueprintBuilder.Dimensions.ToVector2(), containingRectangle);
        }
    }
}
