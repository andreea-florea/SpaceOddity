using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Game.Interfaces;
using System.Collections.Generic;
using NaturalNumbersMath;

namespace Game.Tests
{
    [TestClass]
    public class BlockTest
    {
        private Block block;
        private Mock<IShipComponent> mockShipComponent;
        private List<DoubleEdgedPipe> doubleEdgedPipes;
        private List<ConnectingPipe> oneEdgedPipes;

        [TestInitialize]
        public void Init()
        {
            doubleEdgedPipes = new List<DoubleEdgedPipe>();
            oneEdgedPipes = new List<ConnectingPipe>();
            block = new Block(5, doubleEdgedPipes, oneEdgedPipes);
            mockShipComponent = new Mock<IShipComponent>();
        }

        [TestMethod]
        public void CheckIfShipComponentIsAddedCorrectlyOnBlock()
        {
            block.AddShipComponent(mockShipComponent.Object);
            Assert.AreEqual(mockShipComponent.Object, block.ShipComponent);
        }
        
        [TestMethod]
        public void CheckThatShipComponentIsDeletedFromBlock()
        {
            block.AddShipComponent(mockShipComponent.Object);
            Assert.AreEqual(mockShipComponent.Object, block.ShipComponent);
            block.DeleteShipComponent();
            Assert.AreEqual(null, block.ShipComponent);
        }

        [TestMethod]
        public void CheckIfBlockHasShipComponent()
        {
            block.AddShipComponent(mockShipComponent.Object);
            Assert.IsTrue(block.HasShipComponent());
        }

        [TestMethod]
        public void CheckThatEdgesListsAreSetCorrectly()
        {
            Assert.AreEqual(doubleEdgedPipes, block.PipesWithBothEdges);
            Assert.AreEqual(oneEdgedPipes, block.PipesWithOneEdge);
        }

        [TestMethod]
        public void ChechThatPositionIsSetCorrectly()
        {
            var position = new Coordinate(3, 4);

            block.SetPosition(position);

            Assert.AreEqual(position.X, block.Position.X);
            Assert.AreEqual(position.Y, block.Position.Y);
        }
    }
}
