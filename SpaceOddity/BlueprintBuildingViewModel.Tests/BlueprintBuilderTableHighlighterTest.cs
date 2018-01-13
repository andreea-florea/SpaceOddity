using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.DataStructures;
using ViewModel;

namespace BlueprintBuildingViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderTableHighlighterTest
    {
        private Mock<IActivateableWorldObject> mockPipeLink;
        private Mock<IBlueprintBuilderObjectTable> mockBuilderObjectTable;
        private BlueprintBuilderTableHighlighter builderTableHighlighter;

        [TestInitialize]
        public void Initialize()
        {
            mockPipeLink = new Mock<IActivateableWorldObject>();
            mockBuilderObjectTable = new Mock<IBlueprintBuilderObjectTable>();
            builderTableHighlighter = new BlueprintBuilderTableHighlighter(mockBuilderObjectTable.Object);
        }
        
        [TestMethod]
        public void BuilderHighlighterCanHighlightPipeLinks()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(1, 3);
            var edge = new CoordinatePair(position, connectingPosition);
            mockBuilderObjectTable.Setup(table => table.GetPipeLink(edge)).Returns(mockPipeLink.Object);

            builderTableHighlighter.ActivatePipeLink(edge);
            mockPipeLink.Verify(pipeLink => pipeLink.Activate(), Times.Once());
        }

        [TestMethod]
        public void BuilderHighlighterDeactivatesAllActiveElements()
        {
            var edge = new CoordinatePair(new Coordinate(1, 4), new Coordinate(1, 3));
            mockBuilderObjectTable.Setup(table => table.GetPipeLink(edge)).Returns(mockPipeLink.Object);

            builderTableHighlighter.ActivatePipeLink(edge);
            builderTableHighlighter.DeactivateAll();
            builderTableHighlighter.DeactivateAll();
            mockPipeLink.Verify(pipeLink => pipeLink.Deactivate(), Times.Once());
        }
    }
}
