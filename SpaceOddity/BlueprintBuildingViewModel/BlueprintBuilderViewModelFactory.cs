using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.Controller;
using BlueprintBuildingViewModel.DataStructures;
using ViewModel;
using Algorithm;
using Game.Enums;

namespace BlueprintBuildingViewModel
{
    public class BlueprintBuilderViewModelFactory
    {
        private IViewModelTilesFactory tilesFactory;
        private IRenderableFactory blockFactory;
        private IRenderableFactory batteryFactory;
        private IRenderableFactory emptyComponentFactory;
        private IRenderableFactory pipeLinkFactory;
        private ICurveRenderableFactory pipeFactory;

        public BlueprintBuilderViewModelFactory(IViewModelTilesFactory tilesFactory,
            IRenderableFactory blockFactory,
            IRenderableFactory batteryFactory,
            IRenderableFactory emptyComponentFactory,
            IRenderableFactory pipeLinkFactory,
            ICurveRenderableFactory pipeFactory)
        {
            this.tilesFactory = tilesFactory;
            this.blockFactory = blockFactory;
            this.batteryFactory = batteryFactory;
            this.emptyComponentFactory = emptyComponentFactory;
            this.pipeLinkFactory = pipeLinkFactory;
            this.pipeFactory = pipeFactory;
        }

        public BlueprintBuilderViewModel CreateViewModel(IBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var tiles = tilesFactory.CreateTiles(builder.Dimensions, fittingRectangle);
            var blocks = new Dictionary<Coordinate, IActivateableWorldObject>();
            var shipComponents = new Dictionary<Coordinate, IActivateableWorldObject>();
            var pipeLinks = new Dictionary<CoordinatePair, IActivateableWorldObject>();
            var doubleEdgedPipes = new Dictionary<PipePosition, IWorldObject>();

            var objectTable = new BlueprintBuilderObjectTable(
                tiles, blocks, shipComponents, pipeLinks, doubleEdgedPipes);
            var controlAssigner = CreateController(builder, new BlueprintBuilderTableHighlighter(objectTable));

            var tileRectanle = new CoordinateRectangle(Coordinates.Zero, builder.Dimensions);
            foreach(var coordinate in tileRectanle.Points)
            {
                controlAssigner.AssignTileControl(tiles.Get(coordinate), coordinate);
            }

            var shipComponentsFactories = new Dictionary<BlueprintShipComponentType, IFactory<IActivateableWorldObject>>();
            shipComponentsFactories.Add(BlueprintShipComponentType.Empty, new ActivateableWorldObjectFactory(emptyComponentFactory));
            shipComponentsFactories.Add(BlueprintShipComponentType.Battery, new ActivateableWorldObjectFactory(batteryFactory));

            var viewModel = new BlueprintBuilderViewModel(objectTable,
                new ActivateableWorldObjectFactory(blockFactory), 
                new FactoryPicker<BlueprintShipComponentType, IActivateableWorldObject>(shipComponentsFactories),
                new ActivateableWorldObjectFactory(pipeLinkFactory),
                new CurveWorldObjectFactory(pipeFactory),
                controlAssigner);
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
    }
}
