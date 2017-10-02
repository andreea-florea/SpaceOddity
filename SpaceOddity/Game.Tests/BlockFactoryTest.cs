using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Tests
{
    [TestClass]
    public class BlockFactoryTest
    {
        [TestMethod]
        public void CheckIfBlockFactoryCreatesBlocksWithCorrectWeight()
        {
            var blockFactory = new BlockFactory(5.2);
            var block = blockFactory.CreateBlock();
            Assert.AreEqual(block.Weight, 5.2);

            blockFactory = new BlockFactory(5);
            block = blockFactory.CreateBlock();
            Assert.AreEqual(block.Weight, 5);
        }
    }
}
