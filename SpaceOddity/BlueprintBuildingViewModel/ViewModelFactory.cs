﻿using Game.Interfaces;
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
using Algorithms;
using Game.Enums;

namespace BlueprintBuildingViewModel
{
    public class ViewModelFactory
    {
        private IViewModelTilesFactory tilesFactory;
        private IFactory<IRenderable> blockFactory;
        private IFactory<IRenderable> batteryFactory;
        private IFactory<IRenderable> emptyComponentFactory;
        private IFactory<IRenderable> pipeLinkFactory;
        private IFactory<IRenderable, ICurve> pipeFactory;

        public ViewModelFactory(IViewModelTilesFactory tilesFactory,
            IFactory<IRenderable> blockFactory,
            IFactory<IRenderable> batteryFactory,
            IFactory<IRenderable> emptyComponentFactory,
            IFactory<IRenderable> pipeLinkFactory,
            IFactory<IRenderable, ICurve> pipeFactory)
        {
            this.tilesFactory = tilesFactory;
            this.blockFactory = blockFactory;
            this.batteryFactory = batteryFactory;
            this.emptyComponentFactory = emptyComponentFactory;
            this.pipeLinkFactory = pipeLinkFactory;
            this.pipeFactory = pipeFactory;
        }

        public ViewModel CreateViewModel(IBlueprintBuilder builder, IRectangleSection fittingRectangle)
        {
            var tiles = tilesFactory.CreateTiles(builder.Dimensions, fittingRectangle);
            var objectTable = CreateObjectTable(tiles);
            var controlAssigner = CreateController(builder, new TableHighlighter(objectTable));

            AssignTileControls(builder, tiles, controlAssigner);

            var viewModel = CreateViewModel(objectTable, controlAssigner, CreateShipComponentsFactories());
            builder.AttachObserver(viewModel);
            return viewModel;
        }

        private static ObjectTable CreateObjectTable(IActivateableWorldObject[,] tiles)
        {
            var blocks = new WorldObjectDictionary<Coordinate, IActivateableWorldObject>();
            var shipComponents = new WorldObjectDictionary<Coordinate, IActivateableWorldObject>();
            var pipeLinks = new WorldObjectDictionary<CoordinatePair, IActivateableWorldObject>();
            var doubleEdgedPipes = new WorldObjectDictionary<PipePosition, IWorldObject>();

            return new ObjectTable(tiles, blocks, shipComponents, pipeLinks, doubleEdgedPipes);
        }

        private static void AssignTileControls(IBlueprintBuilder builder, IActivateableWorldObject[,] tiles, ControlAssigner controlAssigner)
        {
            var tileRectanle = new CoordinateRectangle(Coordinates.Zero, builder.Dimensions);
            foreach (var coordinate in tileRectanle.Points)
            {
                controlAssigner.AssignTileControl(tiles.Get(coordinate), coordinate);
            }
        }

        private Dictionary<BlueprintShipComponentType, IFactory<IActivateableWorldObject>> 
            CreateShipComponentsFactories()
        {
            var shipComponentsFactories =
                new Dictionary<BlueprintShipComponentType, IFactory<IActivateableWorldObject>>();
            shipComponentsFactories.Add(
                BlueprintShipComponentType.Empty, new ActivateableWorldObjectFactory(emptyComponentFactory));
            shipComponentsFactories.Add(
                BlueprintShipComponentType.Battery, new ActivateableWorldObjectFactory(batteryFactory));
            return shipComponentsFactories;
        }

        private ViewModel CreateViewModel(
            ObjectTable objectTable, 
            ControlAssigner controlAssigner, 
            Dictionary<BlueprintShipComponentType, IFactory<IActivateableWorldObject>> shipComponentsFactories)
        {
            var viewModel = new ViewModel(
                objectTable,
                new ActivateableWorldObjectFactory(blockFactory),
                new FactoryPicker<BlueprintShipComponentType, IActivateableWorldObject>(shipComponentsFactories),
                new ActivateableWorldObjectFactory(pipeLinkFactory),
                new CurveWorldObjectFactory(pipeFactory),
                controlAssigner);
            return viewModel;
        }

        private ControlAssigner CreateController(IBlueprintBuilder builder,
            ITableHighlighter tableHighlighter)
        {
            var controllerFactory = new ControllerFactory();
            var controller = controllerFactory.CreateController(builder, tableHighlighter);
            var controlAssigner = new ControlAssigner(controller);
            return controlAssigner;
        }
    }
}