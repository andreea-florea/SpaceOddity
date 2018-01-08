using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Controller;
using ViewModel.DataStructures;

namespace ViewModel
{
    public class BlueprintBuilderViewModelFactory
    {
        private IViewModelTilesFactory tilesFactory;
        private IRenderableFactory blockFactory;
        private IRenderableFactory shipComponentsFactory;
        private IRenderableFactory pipeLinkFactory;
        private ICurveRenderableFactory pipeFactory;

        public BlueprintBuilderViewModelFactory(IViewModelTilesFactory tilesFactory,
            IRenderableFactory blockFactory, 
            IRenderableFactory shipComponentsFactory,
            IRenderableFactory pipeLinkFactory,
            ICurveRenderableFactory pipeFactory)
        {
            this.tilesFactory = tilesFactory;
            this.blockFactory = blockFactory;
            this.shipComponentsFactory = shipComponentsFactory;
            this.pipeLinkFactory = pipeLinkFactory;
            this.pipeFactory = pipeFactory;
        }

        public BlueprintBuilderViewModel CreateViewModel(IBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var tiles = tilesFactory.CreateTiles(builder.Dimensions, fittingRectangle);
            var blocks = new IBuilderWorldObject[builder.Dimensions.Y, builder.Dimensions.X];
            var shipComponents = new IBuilderWorldObject[builder.Dimensions.Y, builder.Dimensions.X];
            var pipeLinks = new Dictionary<CoordinatePair, IBuilderWorldObject>();
            var doubleEdgedPipes = new Dictionary<PipePosition, IWorldObject>();

            var objectTable = new BlueprintBuilderObjectTable(
                tiles, blocks, shipComponents, pipeLinks, doubleEdgedPipes);
            var controlAssigner = CreateController(builder, new BlueprintBuilderTableHighlighter(objectTable));

            var tileRectanle = new CoordinateRectangle(Coordinates.Zero, builder.Dimensions);
            foreach(var coordinate in tileRectanle.Points)
            {
                controlAssigner.AssignTileControl(tiles.Get(coordinate), coordinate);
            }

            var viewModel = new BlueprintBuilderViewModel(objectTable,
                new BuilderWorldObjectFactory(blockFactory), 
                new BuilderWorldObjectFactory(shipComponentsFactory),
                new BuilderWorldObjectFactory(pipeLinkFactory),
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
