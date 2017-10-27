using Game.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NaturalNumbersMath;

namespace Game.Tests
{
    [TestClass]
    public class BlueprintBuilderTest
    {
        private IBlock[,] blueprint;
        private BlueprintBuilder blueprintBuilder;
        private Mock<IBlockFactory> mockBlockFactory;
        private Mock<IBlock> mockBlock;
        private Mock<IShipComponent> mockShipComponent;

        [TestInitialize]
        public void Init()
        {
            blueprint = new IBlock[9, 10];
            mockBlock = new Mock<IBlock>();
            mockShipComponent = new Mock<IShipComponent>();
            mockBlockFactory = new Mock<IBlockFactory>();
            blueprintBuilder = new BlueprintBuilder(blueprint, mockBlockFactory.Object);
        }

        [TestMethod]
        public void CheckIfBlueprintMatrixIsAssignedCorrectly()
        {
            var position = new Coordinate(3, 2);
            blueprint[2, 3] = mockBlock.Object;
            Assert.AreEqual(9, blueprintBuilder.Height);
            Assert.AreEqual(10, blueprintBuilder.Width);
            Assert.AreEqual(blueprint[2, 3], blueprintBuilder.GetBlock(position));
        }

        [TestMethod]
        public void CheckIfBlockGetsCreatedCorrectlyOnEmptySpot()
        {
            blueprint[4, 5] = mockBlock.Object;
            Assert.AreNotEqual(blueprint[4, 5], null);
        }

        [TestMethod]
        public void BlockCantBeCreatedIfSpotOccupied()
        {
            var position = new Coordinate(5, 4);
            blueprint[4, 5] = mockBlock.Object;
            Assert.IsFalse(blueprintBuilder.CreateBlock(position));
        }

        [TestMethod]
        public void CheckIfBlockFactoryIsUsedToCreateOtherBlocks()
        {
            var position = new Coordinate(5, 4);
            mockBlockFactory.Setup(x => x.CreateBlock()).Returns(mockBlock.Object);
            Assert.IsTrue(blueprintBuilder.CreateBlock(position));
            Assert.AreEqual(mockBlock.Object, blueprintBuilder.GetBlock(position));
        }

        [TestMethod]
        public void CheckIfExistentBlockIsDeletedSuccessfully()
        {
            var position = new Coordinate(5, 4);
            blueprint[4, 5] = mockBlock.Object;
            Assert.AreNotEqual(null, blueprint[4, 5]);
            Assert.IsTrue(blueprintBuilder.DeleteBlock(position));
            Assert.AreEqual(null, blueprint[4, 5]);
        }

        [TestMethod]
        public void CheckThatInexistentBlockCannotBeDeleted()
        {
            var position = new Coordinate(5, 4);
            Assert.AreEqual(null, blueprint[4, 5]);
            Assert.IsFalse(blueprintBuilder.DeleteBlock(position));
        }

        [TestMethod]
        public void CheckIfShipComponentIsAddedCorrectlyOnBlockOnBlueprint()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns(mockShipComponent.Object);
            blueprint[4, 5] = mockBlock.Object;
            Assert.IsTrue(blueprintBuilder.AddShipComponent(position, mockShipComponent.Object));
            Assert.AreEqual(mockShipComponent.Object, blueprint[4, 5].ShipComponent);
            mockBlock.Verify(x => x.AddShipComponent(It.IsAny<IShipComponent>()), Times.Once());
        }

        [TestMethod]
        public void CheckThatShipComponentCannotBeAddedOnInexistentBlock()
        {
            var position = new Coordinate(5, 4);
            Assert.IsFalse(blueprintBuilder.AddShipComponent(position, mockShipComponent.Object));
            mockBlock.Verify(x => x.AddShipComponent(It.IsAny<IShipComponent>()), Times.Never());
        }

        [TestMethod]
        public void CheckIfShipComponentIsDeletedFromBlockOnBlueprint()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns(mockShipComponent.Object);
            mockBlock.Setup(x => x.DeleteShipComponent()).Callback(() => mockBlock.SetupGet(m => m.ShipComponent).Returns((IShipComponent)null));
            blueprint[4, 5] = mockBlock.Object;
            Assert.IsTrue(blueprintBuilder.AddShipComponent(position, mockShipComponent.Object));
            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            Assert.AreEqual(null, blueprint[4, 5].ShipComponent);
            mockBlock.Verify(x => x.DeleteShipComponent(), Times.Once());
        }

        [TestMethod]
        public void CheckThatShipComponentCannotBeDeletedIfInexistent()
        {
            var position = new Coordinate(5, 4);
            blueprint[4, 5] = mockBlock.Object;
            mockBlock.SetupGet(m => m.ShipComponent).Returns((IShipComponent)null);
            Assert.IsFalse(blueprintBuilder.DeleteShipComponent(position));
            mockBlock.Verify(x => x.DeleteShipComponent(), Times.Never());
        }

        [TestMethod]
        public void CheckThatShipComponentCannotBeDeletedFromInexistentBlock()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns((IShipComponent)null);
            Assert.IsFalse(blueprintBuilder.DeleteShipComponent(position));
            mockBlock.Verify(x => x.DeleteShipComponent(), Times.Never());
        }
    }
}
