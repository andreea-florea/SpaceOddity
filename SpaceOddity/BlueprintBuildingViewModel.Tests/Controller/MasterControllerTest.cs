using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.Controller;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.DataStructures;

namespace BlueprintBuildingViewModel.Tests.Controller
{
    [TestClass]
    public class MasterControllerTest
    {
        private Mock<IController> mockBaseController;
        private Mock<IController> mockController;
        private Mock<ITableHighlighter> mockTableHighlighter;
        private MasterController controller;

        [TestInitialize]
        public void Initialize()
        {
            mockBaseController = new Mock<IController>();
            mockController = new Mock<IController>();
            mockTableHighlighter = new Mock<ITableHighlighter>();
            controller = new MasterController(
                mockBaseController.Object, mockController.Object, mockTableHighlighter.Object);
        }

        [TestMethod]
        public void BaseControllerIsCalledOnBlockSelected()
        {
            var position = new Coordinate(2, 1);
            controller.SelectBlock(position);
            mockController.Verify(baseController => baseController.SelectBlock(position), Times.Once());
        }

        [TestMethod]
        public void BaseControllerIsCalledOnBlockCancelled()
        {
            var position = new Coordinate(2, 1);
            controller.CancelBlock(position);
            mockController.Verify(baseController => baseController.CancelBlock(position), Times.Once());
        }

        [TestMethod]
        public void BaseControllerIsCalledOnTileSelected()
        {
            var position = new Coordinate(2, 1);
            controller.SelectTile(position);
            mockController.Verify(baseController => baseController.SelectTile(position), Times.Once());
        }

        [TestMethod]
        public void BaseControllerIsCalledOnShipComponentSelect()
        {
            var position = new Coordinate(2, 1);
            controller.SelectShipComponent(position);
            mockController.Verify(baseController => baseController.SelectShipComponent(position), Times.Once());
        }

        [TestMethod]
        public void BaseControllerIsCalledOnShipComponentCancel()
        {
            var position = new Coordinate(2, 1);
            controller.CancelShipComponent(position);
            mockController.Verify(baseController => baseController.CancelShipComponent(position), Times.Once());
        }

        [TestMethod]
        public void BaseControllerIsCalledOnPipeLinkSelected()
        {
            var edge = new CoordinatePair(new Coordinate(2, 1), new Coordinate(2, 3));
            controller.SelectPipeLink(edge);
            mockController.Verify(baseController => baseController.SelectPipeLink(edge), Times.Once());
        }

        [TestMethod]
        public void UseBaseControllerAfterRest()
        {
            var position = new Coordinate(2, 1);
            controller.Reset();
            controller.SelectTile(position);
            mockBaseController.Verify(baseController => baseController.SelectTile(position), Times.Once());
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
