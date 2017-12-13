using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;
using NaturalNumbersMath;
using ViewModel.DataStructures;
using Game;
using ViewModel.Controller;

namespace ViewModel.Tests.Controller
{
    [TestClass]
    public class BlueprintBuilderBasicControllerTest
    {
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprintBuilderTableHighlighter> mockTableHighlighter;
        private BlueprintBuilderMasterController masterController;
        private BlueprintBuilderPipeBuildController pipeBuildController;
        private BlueprintBuilderBasicController controller;

        [TestInitialize]
        public void Initialize()
        {
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockTableHighlighter = new Mock<IBlueprintBuilderTableHighlighter>();
            masterController = new BlueprintBuilderMasterController(null, null, mockTableHighlighter.Object);
            pipeBuildController = new BlueprintBuilderPipeBuildController(masterController, mockBlueprintBuilder.Object, new CoordinatePair());
            controller = new BlueprintBuilderBasicController(masterController, pipeBuildController, mockBlueprintBuilder.Object);
            masterController.BaseController = controller;
            masterController.Reset();
        }

        [TestMethod]
        public void CreateBlocksOnTileSelect()
        {
            var position = new Coordinate(4, 2);
            controller.TileSelect(position);
            mockBlueprintBuilder.Verify(builder => builder.CreateBlock(position));
        }

        [TestMethod]
        public void DeletesBlockOnBlockCancel()
        {
            var position = new Coordinate(4, 2);
            controller.BlockCancel(position);
            mockBlueprintBuilder.Verify(builder => builder.DeleteBlock(position));
        }

        [TestMethod]
        public void CreatesShipComponentOnBlockSelect()
        {
            var position = new Coordinate(4, 2);
            controller.BlockSelect(position);
            mockBlueprintBuilder.Verify(builder => builder.AddShipComponent(position));
        }

        [TestMethod]
        public void SetCurrentControllerCorrectlyOnPipeSelect()
        {
            var edge = new CoordinatePair(new Coordinate(2, 3), new Coordinate(2, 4));
            controller.PipeLinkSelect(edge);
            Assert.AreEqual(edge, pipeBuildController.SelectedLink);
            Assert.AreEqual(masterController.CurrentController, pipeBuildController);
        }
    }
}
