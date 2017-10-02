using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Tests.Mocks;

namespace Game.Tests
{
    [TestClass]
    public class BlueprintBuilderTest
    {
        private IBlock[,] blueprint;
        private BlueprintBuilder blueprintBuilder;
        private MockBlockFactory mockBlockFactory;

        [TestInitialize]
        public void Init()
        {
            blueprint = new IBlock[9, 10];
            mockBlockFactory = new MockBlockFactory(new MockBlock());
            blueprintBuilder = new BlueprintBuilder(blueprint, mockBlockFactory);
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
            Assert.IsTrue(blueprintBuilder.CreateBlock(4, 5));
            Assert.AreNotEqual(blueprint[4, 5], null);
        }

        [TestMethod]
        public void BlockCantBeCreatedIfSpotOccupied()
        {
            Assert.IsTrue(blueprintBuilder.CreateBlock(4, 5));
            Assert.IsFalse(blueprintBuilder.CreateBlock(4, 5));
        }

        [TestMethod]
        public void CheckIfBlockFactoryIsUsedToCreateOtherBlocks()
        {
            Assert.IsTrue(blueprintBuilder.CreateBlock(4, 5));
            Assert.AreEqual(mockBlockFactory.Block, blueprintBuilder.GetBlock(4, 5));
        }
    }
}
