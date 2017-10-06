using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Game.Interfaces;

namespace Game.Tests
{
    [TestClass]
    public class BlockTest
    {
        private Block block;
        private Mock<IShipComponent> mockShipComponent;

        [TestInitialize]
        public void Init()
        {
            block = new Block(5);
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
    }
}
