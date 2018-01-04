using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Controller;
using Game.Interfaces;
using Moq;
using NaturalNumbersMath;
using ViewModel.DataStructures;
using Game;

namespace ViewModel.Tests.Controller
{
    [TestClass]
    public class BlueprintBuilderControllerFactoryTest
    {
        private Mock<IBlock> mockBlock;
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprintBuilderTableHighlighter> mockTableHighlighter;
        private BlueprintBuilderControllerFactory factory;
        private IBlueprintBuilderController controller;

        [TestInitialize]
        public void Initialize()
        {
            mockBlock = new Mock<IBlock>();
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockTableHighlighter = new Mock<IBlueprintBuilderTableHighlighter>();
            factory = new BlueprintBuilderControllerFactory();
            controller = factory.CreateController(mockBlueprintBuilder.Object, mockTableHighlighter.Object);
        }

        [TestMethod]
        public void CreateMasterController()
        {
            var position = new Coordinate(2, 3);

            controller.TileSelect(position);

            mockBlueprintBuilder.Verify(builder => builder.CreateBlock(position), Times.Once());
        }

        [TestMethod]
        public void CreateDoubleEdgedPipeBySelectingTwoPipeLinks()
        {
            var position = new Coordinate(2, 3);
            var edge1 = new CoordinatePair(position, new Coordinate(2, 4));
            var edge2 = new CoordinatePair(position, new Coordinate(2, 2));
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);

            controller.PipeLinkSelect(edge1);
            controller.PipeLinkSelect(edge2);
            
            mockBlueprintBuilder.Verify(builder => builder.AddDoubleEdgedPipe(position, EdgeType.UP, EdgeType.DOWN), Times.Once());
        }

        [TestMethod]
        public void CreateAnotherDoubleEdgedPipeBySelectingTwoPipeLinks()
        {
            var position = new Coordinate(2, 3);
            var edge1 = new CoordinatePair(position, new Coordinate(2, 4));
            var edge2 = new CoordinatePair(position, new Coordinate(3, 3));
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);

            controller.PipeLinkSelect(edge1);
            controller.PipeLinkSelect(edge2);

            mockBlueprintBuilder.Verify(builder => builder.AddDoubleEdgedPipe(position, EdgeType.UP, EdgeType.RIGHT), Times.Once());
        }
    }
}
