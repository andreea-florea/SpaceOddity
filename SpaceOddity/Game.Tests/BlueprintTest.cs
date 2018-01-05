using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Game.Interfaces;
using NaturalNumbersMath;
using Game.Enums;

namespace Game.Tests
{
    [TestClass]
    public class BlueprintTest
    {
        private Blueprint blueprint;
        private IBlock[,] blocks;
        private Mock<IBlock> mockBlock;
        private Mock<IBlueprintObserver> mockObserver;
        private Mock<IShipComponent> mockShipComponent;

        [TestInitialize]
        public void Init()
        {
            blocks = new IBlock[6, 7];
            blueprint = new Blueprint(blocks);
            mockBlock = new Mock<IBlock>();
            mockShipComponent = new Mock<IShipComponent>();
            mockObserver = new Mock<IBlueprintObserver>();
            blueprint.AttachObserver(mockObserver.Object);
        }

        [TestMethod]
        public void CheckThatBlockIsSetCorrectly()
        {
            var position = new Coordinate(3, 4);
            blueprint.PlaceBlock(position, mockBlock.Object);
            Assert.AreEqual(mockBlock.Object, blueprint.GetBlock(position));
        }

        [TestMethod]
        public void CheckThatPositionOfBlockIsSetWhenCreatingBlock()
        {
            var position = new Coordinate(1, 2);
          
            blueprint.PlaceBlock(position, mockBlock.Object);

            mockBlock.Verify(block => block.SetPosition(position), Times.Once());
        }

        [TestMethod]
        public void CheckThatBlockIsRemoved()
        {
            var position = new Coordinate(3, 4);
            blueprint.PlaceBlock(position, mockBlock.Object);
            blueprint.RemoveBlock(position);
            Assert.AreEqual(null, blueprint.GetBlock(position));
        }

        [TestMethod]
        public void CheckThatPlacingConnectingPipeCallsbserver()
        {
            var position = new Coordinate(3, 4);
            var pipe = new ConnectingPipe(EdgeType.RIGHT);

            blueprint.PlaceBlock(position, mockBlock.Object);
            blueprint.PlacePipe(position, pipe);

            mockObserver.Verify(obs => obs.ConnectingPipeAdded(It.IsAny<IBlueprint>(), position, pipe), Times.Once());
            mockBlock.Verify(block => block.AddPipe(pipe), Times.Once());
        }

        [TestMethod]
        public void CheckThatRemovingConnectingPipeCallsbserver()
        {
            var position = new Coordinate(3, 4);
            var pipe = new ConnectingPipe(EdgeType.RIGHT);

            blueprint.PlaceBlock(position, mockBlock.Object);
            blueprint.PlacePipe(position, pipe);
            blueprint.RemovePipe(position, pipe);

            mockObserver.Verify(obs => obs.ConnectingPipeAdded(It.IsAny<IBlueprint>(), position, pipe), Times.Once());
            mockObserver.Verify(obs => obs.ConnectingPipeDeleted(It.IsAny<IBlueprint>(), position, pipe), Times.Once());
        }

        [TestMethod]
        public void CheckThatPlacingDoubleEdgedPipeCallsObserver()
        {
            var position = new Coordinate(3, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.RIGHT, EdgeType.DOWN);

            blueprint.PlaceBlock(position, mockBlock.Object);
            blueprint.PlacePipe(position, pipe);

            mockObserver.Verify(obs => obs.DoubleEdgePipeAdded(It.IsAny<IBlueprint>(), position, pipe), Times.Once());
            mockBlock.Verify(block => block.AddPipe(pipe), Times.Once());
        }

        [TestMethod]
        public void CheckThatRemovingDoubleEdgedPipeCallsObserver()
        {
            var position = new Coordinate(3, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.RIGHT, EdgeType.DOWN);

            blueprint.PlaceBlock(position, mockBlock.Object);
            blueprint.PlacePipe(position, pipe);
            blueprint.RemovePipe(position, pipe);

            mockObserver.Verify(obs => obs.DoubleEdgePipeAdded(It.IsAny<IBlueprint>(), position, pipe), Times.Once());
            mockObserver.Verify(obs => obs.DoubleEdgePipeDeleted(It.IsAny<IBlueprint>(), position, pipe), Times.Once());
        }

        [TestMethod]
        public void CheckThatPlacingShipComponentCallsObserver()
        {
            var position = new Coordinate(3, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.RIGHT, EdgeType.DOWN);

            blueprint.PlaceBlock(position, mockBlock.Object);
            blueprint.PlacePipe(position, pipe);
            blueprint.PlaceShipComponent(position, mockShipComponent.Object);

            mockObserver.Verify(obs => obs.ShipComponentAdded(It.IsAny<IBlueprint>(), position), Times.Once());
        }

        [TestMethod]
        public void CheckThatRemovingShipComponentCallsObserver()
        {
            var position = new Coordinate(3, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.RIGHT, EdgeType.DOWN);

            blueprint.PlaceBlock(position, mockBlock.Object);
            blueprint.PlacePipe(position, pipe);
            blueprint.PlaceShipComponent(position, mockShipComponent.Object);
            blueprint.RemoveShipComponent(position);

            mockObserver.Verify(obs => obs.ShipComponentAdded(It.IsAny<IBlueprint>(), position), Times.Once());
            mockObserver.Verify(obs => obs.ShipComponentDeleted(It.IsAny<IBlueprint>(), position), Times.Once());
        }
    }
}
