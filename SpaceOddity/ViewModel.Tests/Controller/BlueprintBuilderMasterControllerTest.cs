using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Controller;
using Moq;
using NaturalNumbersMath;
using ViewModel.DataStructures;

namespace ViewModel.Tests.Controller
{
    [TestClass]
    public class BlueprintBuilderMasterControllerTest
    {
        private Mock<IBlueprintBuilderController> mockBaseController;
        private Mock<IBlueprintBuilderController> mockController;
        private BlueprintBuilderMasterController controller;

        [TestInitialize]
        public void Initialize()
        {
            mockBaseController = new Mock<IBlueprintBuilderController>();
            mockController = new Mock<IBlueprintBuilderController>();
            controller = new BlueprintBuilderMasterController(mockBaseController.Object, mockController.Object);
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
    }
}
