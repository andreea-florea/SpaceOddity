using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.Controller;
using Game.Interfaces;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.DataStructures;
using Game.Enums;

namespace BlueprintBuildingViewModel.Tests.Controller
{
    [TestClass]
    public class ControllerFactoryTest
    {
        private Mock<IBlock> mockBlock;
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<ITableHighlighter> mockTableHighlighter;
        private ControllerFactory factory;
        private IController controller;

        [TestInitialize]
        public void Initialize()
        {
            mockBlock = new Mock<IBlock>();
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockTableHighlighter = new Mock<ITableHighlighter>();
            factory = new ControllerFactory();
            controller = factory.CreateController(mockBlueprintBuilder.Object, mockTableHighlighter.Object);
        }

        [TestMethod]
        public void CreateMasterController()
        {
            var position = new Coordinate(2, 3);

            controller.SelectTile(position);

            mockBlueprintBuilder.Verify(builder => builder.CreateBlock(position), Times.Once());
        }

        [TestMethod]
        public void CreateDoubleEdgedPipeBySelectingTwoPipeLinks()
        {
            var position = new Coordinate(2, 3);
            var edge1 = new CoordinatePair(position, new Coordinate(2, 4));
            var edge2 = new CoordinatePair(position, new Coordinate(2, 2));
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);

            controller.SelectPipeLink(edge1);
            controller.SelectPipeLink(edge2);
            
            mockBlueprintBuilder.Verify(builder => builder.AddDoubleEdgedPipe(position, EdgeType.UP, EdgeType.DOWN), Times.Once());
        }

        [TestMethod]
        public void CreateAnotherDoubleEdgedPipeBySelectingTwoPipeLinks()
        {
            var position = new Coordinate(2, 3);
            var edge1 = new CoordinatePair(position, new Coordinate(2, 4));
            var edge2 = new CoordinatePair(position, new Coordinate(3, 3));
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);

            controller.SelectPipeLink(edge1);
            controller.SelectPipeLink(edge2);

            mockBlueprintBuilder.Verify(builder => builder.AddDoubleEdgedPipe(position, EdgeType.UP, EdgeType.RIGHT), Times.Once());
        }
    }
}
