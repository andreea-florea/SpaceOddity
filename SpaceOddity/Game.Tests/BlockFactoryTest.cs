using Game.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Game.Tests
{
    [TestClass]
    public class BlockFactoryTest
    {
        [TestMethod]
        public void CheckIfBlockFactoryCreatesBlocksWithCorrectWeight()
        {
            var blockFactory = new BlockFactory(5.2, null);
            var block = blockFactory.CreateBlock();
            Assert.AreEqual(block.Weight, 5.2);

            blockFactory = new BlockFactory(5, null);
            block = blockFactory.CreateBlock();
            Assert.AreEqual(block.Weight, 5);
        }

        [TestMethod]
        public void CheckIfBlockFactoryCreatesBlocksWithCorrectShipComponent()
        {
            var mockShipComponent = new Mock<IShipComponent>();

            var blockFactory = new BlockFactory(5.2, mockShipComponent.Object);
            var block = blockFactory.CreateBlock();
            Assert.AreEqual(mockShipComponent.Object, block.ShipComponent);
        }
    }
}
