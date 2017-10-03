using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Tests.Mocks;
using Moq;

namespace Game.Tests
{
    [TestClass]
    public class BlueprintBuilderTest
    {
        private IBlock[,] blueprint;
        private BlueprintBuilder blueprintBuilder;
        private Mock<IBlockFactory> mockBlockFactory;
        private Mock<IBlock> mockBlock;

        [TestInitialize]
        public void Init()
        {
            blueprint = new IBlock[9, 10];
            mockBlock = new Mock<IBlock>();
            mockBlockFactory = new Mock<IBlockFactory>();
            blueprintBuilder = new BlueprintBuilder(blueprint, mockBlockFactory.Object);
        }

        [TestMethod]
        public void CheckIfBlueprintMatrixIsAssignedCorrectly()
        {
            blueprint[2, 3] = new MockBlock();
            Assert.AreEqual(9, blueprintBuilder.Height);
            Assert.AreEqual(10, blueprintBuilder.Width);
            Assert.AreEqual(blueprint[2, 3], blueprintBuilder.GetBlock(2, 3));
        }

        [TestMethod]
        public void CheckIfBlockGetsCreatedCorrectlyOnEmptySpot()
        {
            blueprint[4, 5] = new Mock<IBlock>().Object;
            Assert.AreNotEqual(blueprint[4, 5], null);
        }

        [TestMethod]
        public void BlockCantBeCreatedIfSpotOccupied()
        {
            blueprint[4, 5] = new Mock<IBlock>().Object;
            Assert.IsFalse(blueprintBuilder.CreateBlock(4, 5));
        }

        [TestMethod]
        public void CheckIfBlockFactoryIsUsedToCreateOtherBlocks()
        {
            MockBlock mockBlock = new MockBlock();
            mockBlockFactory.Setup(x => x.CreateBlock()).Returns(mockBlock);
            Assert.IsTrue(blueprintBuilder.CreateBlock(4, 5));
            Assert.AreEqual(mockBlock, blueprintBuilder.GetBlock(4, 5));
        }

        [TestMethod]
        public void CheckIfExistentBlockIsDeletedSuccessfully()
        {
            blueprint[4, 5] = new Mock<IBlock>().Object;
            Assert.AreNotEqual(null, blueprint[4, 5]);
            Assert.IsTrue(blueprintBuilder.DeleteBlock(4, 5));
            Assert.AreEqual(null, blueprint[4, 5]);
        }

        [TestMethod]
        public void CheckThatInexistentBlockCannotBeDeleted()
        {
            Assert.AreEqual(null, blueprint[4, 5]);
            Assert.IsFalse(blueprintBuilder.DeleteBlock(4, 5));
        }
    }
}
