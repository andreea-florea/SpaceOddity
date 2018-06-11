using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.DataStructures;
using Game;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Tests.Controller
{
    [TestClass]
    public class BasicControllerTest
    {
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<ITableHighlighter> mockTableHighlighter;
        private MasterController masterController;
        private PipeBuildController pipeBuildController;
        private BasicController controller;

        [TestInitialize]
        public void Initialize()
        {
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockTableHighlighter = new Mock<ITableHighlighter>();
            masterController = new MasterController(null, null, mockTableHighlighter.Object);
            pipeBuildController = new PipeBuildController(masterController, mockBlueprintBuilder.Object, new CoordinatePair());
            controller = new BasicController(masterController, pipeBuildController, mockBlueprintBuilder.Object);
            masterController.BaseController = controller;
            masterController.Reset();
        }

        [TestMethod]
        public void CreateBlocksOnTileSelect()
        {
            var position = new Coordinate(4, 2);
            controller.SelectTile(position);
            mockBlueprintBuilder.Verify(builder => builder.CreateBlock(position));
        }

        [TestMethod]
        public void DeletesBlockOnBlockCancel()
        {
            var position = new Coordinate(4, 2);
            controller.CancelBlock(position);
            mockBlueprintBuilder.Verify(builder => builder.DeleteBlock(position));
        }

        [TestMethod]
        public void CreatesShipComponentOnBlockSelect()
        {
            var position = new Coordinate(4, 2);
            controller.SelectBlock(position);
            mockBlueprintBuilder.Verify(builder => builder.CreateShipComponent(position));
        }

        [TestMethod]
        public void DeletesShipComponentOnShipComponentCancel()
        {
            var position = new Coordinate(4, 2);
            controller.CancelShipComponent(position);
            mockBlueprintBuilder.Verify(builder => builder.DeleteShipComponent(position));
        }

        [TestMethod]
        public void SetCurrentControllerCorrectlyOnPipeSelect()
        {
            var edge = new CoordinatePair(new Coordinate(2, 3), new Coordinate(2, 4));
            controller.SelectPipeLink(edge);
            Assert.AreEqual(edge, pipeBuildController.SelectedLink);
            Assert.AreEqual(masterController.CurrentController, pipeBuildController);
        }
    }
}
