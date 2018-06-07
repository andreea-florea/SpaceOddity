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
            var blockFactory = new BlockFactory(5.2);
            var block = blockFactory.Create();
            Assert.AreEqual(block.Weight, 5.2);

            blockFactory = new BlockFactory(5);
            block = blockFactory.Create();
            Assert.AreEqual(block.Weight, 5);
        }
    }
}
