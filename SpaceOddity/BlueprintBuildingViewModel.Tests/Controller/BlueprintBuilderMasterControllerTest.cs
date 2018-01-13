using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.Controller;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.DataStructures;

namespace BlueprintBuildingViewModel.Tests.Controller
{
    [TestClass]
    public class BlueprintBuilderMasterControllerTest
    {
        private Mock<IBlueprintBuilderController> mockBaseController;
        private Mock<IBlueprintBuilderController> mockController;
        private Mock<IBlueprintBuilderTableHighlighter> mockTableHighlighter;
        private BlueprintBuilderMasterController controller;

        [TestInitialize]
        public void Initialize()
        {
            mockBaseController = new Mock<IBlueprintBuilderController>();
            mockController = new Mock<IBlueprintBuilderController>();
            mockTableHighlighter = new Mock<IBlueprintBuilderTableHighlighter>();
            controller = new BlueprintBuilderMasterController(
                mockBaseController.Object, mockController.Object, mockTableHighlighter.Object);
        }

        [TestMethod]
        public void BaseControllerIsCalledOnBlockSelected()
        {
            var position = new Coordinate(2, 1);
            controller.BlockSelect(position);
            mockController.Verify(baseController => baseController.BlockSelect(position), Times.Once());
        }

        [TestMethod]
        public void BaseControllerIsCalledOnBlockCancelled()
        {
            var position = new Coordinate(2, 1);
            controller.BlockCancel(position);
            mockController.Verify(baseController => baseController.BlockCancel(position), Times.Once());
        }

        [TestMethod]
        public void BaseControllerIsCalledOnTileSelected()
        {
            var position = new Coordinate(2, 1);
            controller.TileSelect(position);
            mockController.Verify(baseController => baseController.TileSelect(position), Times.Once());
        }

        [TestMethod]
        public void BaseControllerIsCalledOnShipComponentSelect()
        {
            var position = new Coordinate(2, 1);
            controller.ShipComponentSelect(position);
            mockController.Verify(baseController => baseController.ShipComponentSelect(position), Times.Once());
        }

        [TestMethod]
        public void BaseControllerIsCalledOnShipComponentCancel()
        {
            var position = new Coordinate(2, 1);
            controller.ShipComponentCancel(position);
            mockController.Verify(baseController => baseController.ShipComponentCancel(position), Times.Once());
        }

        [TestMethod]
        public void BaseControllerIsCalledOnPipeLinkSelected()
        {
            var edge = new CoordinatePair(new Coordinate(2, 1), new Coordinate(2, 3));
            controller.PipeLinkSelect(edge);
            mockController.Verify(baseController => baseController.PipeLinkSelect(edge), Times.Once());
        }

        [TestMethod]
        public void UseBaseControllerAfterRest()
        {
            var position = new Coordinate(2, 1);
            controller.Reset();
            controller.TileSelect(position);
            mockBaseController.Verify(baseController => baseController.TileSelect(position), Times.Once());
        }

        [TestMethod]
        public void MasterControllerAllowsObjectActivation()
        {
            var edge = new CoordinatePair(new Coordinate(1, 2), new Coordinate(2, 2));
            controller.ActivatePipeLink(edge);
            mockTableHighlighter.Verify(table => table.ActivatePipeLink(edge), Times.Once());
        }

        [TestMethod]
        public void MasterControllerDeactivatesAllObjectsOnReset()
        {
            controller.Reset();
            mockTableHighlighter.Verify(table => table.DeactivateAll(), Times.Once());
        }
    }
}
