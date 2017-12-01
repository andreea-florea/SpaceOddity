using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Game.Interfaces;
using ViewModel.Controller;
using NaturalNumbersMath;
using ViewModel.DataStructures;
using Game;

namespace ViewModel.Tests.Controller
{
    [TestClass]
    public class BlueprintBuilderPipeBuildControllerTest
    {
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private BlueprintBuilderMasterController masterController;
        private BlueprintBuilderPipeBuildController pipeBuildController;
        private BlueprintBuilderBasicController basicController;

        [TestInitialize]
        public void Initialize()
        {
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            masterController = new BlueprintBuilderMasterController(null, null);
            pipeBuildController = new BlueprintBuilderPipeBuildController(masterController, mockBlueprintBuilder.Object, new CoordinatePair());
            basicController = new BlueprintBuilderBasicController(masterController, pipeBuildController, mockBlueprintBuilder.Object);
            masterController.BaseController = basicController;
            masterController.Reset();
        }

        [TestMethod]
        public void CreateDoubleEdgedPipeBySelectingTwoPipeLinks()
        {
            var position = new Coordinate(2, 3);
            var edge1 = new CoordinatePair(position, new Coordinate(2, 4));
            var edge2 = new CoordinatePair(position, new Coordinate(2, 2));
            pipeBuildController.SelectedLink = edge1;
            pipeBuildController.PipeLinkSelect(edge2);
            mockBlueprintBuilder.Verify(builder => builder.AddDoubleEdgedPipe(position, EdgeType.UP, EdgeType.DOWN), Times.Once());
        }

        [TestMethod]
        public void MasterControllerResetsAfterPipeLinkIsSelected()
        {
            var edge = new CoordinatePair(new Coordinate(2, 3), new Coordinate(2, 4));
            masterController.CurrentController = pipeBuildController;

            pipeBuildController.SelectedLink = new CoordinatePair(new Coordinate(2, 3), new Coordinate(3, 3));
            pipeBuildController.PipeLinkSelect(edge);

            Assert.AreEqual(masterController.BaseController, masterController.CurrentController);
        }
    }
}
