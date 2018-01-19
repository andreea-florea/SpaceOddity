using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Game.Interfaces;
using BlueprintBuildingViewModel.Controller;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.DataStructures;
using Game;
using Game.Enums;
using System.Collections.Generic;

namespace BlueprintBuildingViewModel.Tests.Controller
{
    [TestClass]
    public class PipeBuildControllerTest
    {
        private Mock<IBlock> mockBlock;
        private List<DoubleEdgedPipe> doubleEdgedPipes;
        private List<ConnectingPipe> connectingPipes;
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<ITableHighlighter> mockHighlighter;
        private MasterController masterController;
        private PipeBuildController pipeBuildController;
        private BasicController basicController;

        [TestInitialize]
        public void Initialize()
        {
            mockBlock = new Mock<IBlock>();
            doubleEdgedPipes = new List<DoubleEdgedPipe>();
            mockBlock.SetupGet(block => block.PipesWithBothEdges).Returns(doubleEdgedPipes);
            connectingPipes = new List<ConnectingPipe>();
            mockBlock.SetupGet(block => block.PipesWithOneEdge).Returns(connectingPipes);

            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockHighlighter = new Mock<ITableHighlighter>();
            masterController = new MasterController(null, null, mockHighlighter.Object);
            pipeBuildController = new PipeBuildController(masterController, mockBlueprintBuilder.Object, new CoordinatePair());
            basicController = new BasicController(masterController, pipeBuildController, mockBlueprintBuilder.Object);
            masterController.BaseController = basicController;
            masterController.Reset();
        }

        [TestMethod]
        public void CreateDoubleEdgedPipeBySelectingTwoPipeLinks()
        {
            var position = new Coordinate(2, 3);
            var edge1 = new CoordinatePair(position, new Coordinate(2, 4));
            var edge2 = new CoordinatePair(position, new Coordinate(2, 2));
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);

            pipeBuildController.SelectedLink = edge1;
            pipeBuildController.SelectPipeLink(edge2);
            mockBlueprintBuilder.Verify(builder => builder.AddDoubleEdgedPipe(position, EdgeType.UP, EdgeType.DOWN), Times.Once());
        }

        [TestMethod]
        public void MasterControllerResetsAfterPipeLinkIsSelected()
        {
            var edge = new CoordinatePair(new Coordinate(2, 5), new Coordinate(2, 4));
            masterController.CurrentController = pipeBuildController;

            pipeBuildController.SelectedLink = new CoordinatePair(new Coordinate(2, 3), new Coordinate(3, 3));
            pipeBuildController.SelectPipeLink(edge);

            Assert.AreEqual(masterController.BaseController, masterController.CurrentController);
        }

        [TestMethod]
        public void MasterControllerDoesNotCreatePipeIfLinkFromDifferentBlockIsSelected()
        {
            var edge1 = new CoordinatePair(new Coordinate(2, 3), new Coordinate(2, 4));
            var edge2 = new CoordinatePair(new Coordinate(3, 2), new Coordinate(2, 2));
            
            pipeBuildController.SelectedLink = edge1;
            pipeBuildController.SelectPipeLink(edge2);

            mockBlueprintBuilder.Verify(builder => 
                builder.AddDoubleEdgedPipe(It.IsAny<Coordinate>(), It.IsAny<EdgeType>(), It.IsAny<EdgeType>()), Times.Never());
        }

        [TestMethod]
        public void IfSelectedPipeLinkIsSelectedAgainItResetsToTheMasterController()
        {
            var edge = new CoordinatePair(new Coordinate(2, 3), new Coordinate(2, 4));

            pipeBuildController.SelectedLink = edge;
            pipeBuildController.SelectPipeLink(edge);

            mockBlueprintBuilder.Verify(builder =>
                builder.AddDoubleEdgedPipe(It.IsAny<Coordinate>(), It.IsAny<EdgeType>(), It.IsAny<EdgeType>()), Times.Never());
        }

        [TestMethod]
        public void PipeLinkObjectIsActivated()
        {
            var edge = new CoordinatePair(new Coordinate(2, 3), new Coordinate(2, 4));
            pipeBuildController.SelectedLink = edge;
            mockHighlighter.Verify(highlighter => highlighter.ActivatePipeLink(edge), Times.Once());
        }

        [TestMethod]
        public void DeleteAlreadyCreatedPipe()
        {
            var position = new Coordinate(2, 3);
            var edge = new CoordinatePair(position, new Coordinate(2, 4));
            var otherEdge = new CoordinatePair(position, new Coordinate(2, 2));

            doubleEdgedPipes.Add(new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP));
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);

            DoubleEdgedPipe pipeParameter = null;
            mockBlueprintBuilder
                .Setup(builder => builder.DeleteDoubleEdgedPipe(position, It.IsAny<DoubleEdgedPipe>()))
                .Callback<Coordinate, DoubleEdgedPipe>((coord, param) => pipeParameter = param);

            pipeBuildController.SelectedLink = edge;
            pipeBuildController.SelectPipeLink(otherEdge);

            Assert.AreEqual(EdgeType.UP, pipeParameter.FirstEdge);
            Assert.AreEqual(EdgeType.DOWN, pipeParameter.SecondEdge);
        }


        [TestMethod]
        public void DeleteAlreadyCreatedConnectingPipesWhenDeletingDoubleEdgedPipe()
        {
            var position = new Coordinate(2, 3);
            var edge = new CoordinatePair(position, new Coordinate(2, 4));
            var otherEdge = new CoordinatePair(position, new Coordinate(2, 2));

            connectingPipes.Add(new ConnectingPipe(EdgeType.UP));
            connectingPipes.Add(new ConnectingPipe(EdgeType.DOWN));
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);

            DoubleEdgedPipe pipeParameter = null;
            mockBlueprintBuilder
                .Setup(builder => builder.DeleteDoubleEdgedPipe(position, It.IsAny<DoubleEdgedPipe>()))
                .Callback<Coordinate, DoubleEdgedPipe>((coord, param) => pipeParameter = param);

            pipeBuildController.SelectedLink = edge;
            pipeBuildController.SelectPipeLink(otherEdge);

            Assert.AreEqual(EdgeType.UP, pipeParameter.FirstEdge);
            Assert.AreEqual(EdgeType.DOWN, pipeParameter.SecondEdge);
        }

        [TestMethod]
        public void CanCreateConnectingPipeToShipComponent()
        {
            var position = new Coordinate(2, 3);

            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);

            masterController.CurrentController = pipeBuildController;
            pipeBuildController.SelectedLink = new CoordinatePair(new Coordinate(2, 2), position);
            pipeBuildController.SelectShipComponent(position);

            mockBlueprintBuilder.Verify(builder => builder.AddConnectingPipe(position, EdgeType.DOWN), Times.Once());
            Assert.AreNotEqual(masterController.CurrentController, pipeBuildController);
        }

        [TestMethod]
        public void CantCreateConnectingPipeToShipComponentFromOtherBlock()
        {
            var position = new Coordinate(2, 3);

            pipeBuildController.SelectedLink = new CoordinatePair(new Coordinate(2, 1), new Coordinate(2, 2));
            pipeBuildController.SelectShipComponent(position);

            mockBlueprintBuilder.Verify(builder => builder.AddConnectingPipe(position, EdgeType.DOWN), Times.Never());
        }

        [TestMethod]
        public void DeleteConnectingPipeCorrectly()
        {
            var position = new Coordinate(3, 2);

            connectingPipes.Add(new ConnectingPipe(EdgeType.LEFT));
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);

            pipeBuildController.SelectedLink = new CoordinatePair(new Coordinate(2, 2), position);
            pipeBuildController.SelectShipComponent(position);

            mockBlueprintBuilder.Verify(builder => builder.DeleteConnectingPipe(position, new ConnectingPipe(EdgeType.LEFT)), Times.Once());
        }
    }
}
