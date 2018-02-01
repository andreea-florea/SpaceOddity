using System;
using Game.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Game.Tests
{
    [TestClass]
    public class EmptyShipComponentFactoryTest
    {
        private Mock<IBlock> mockBlock;

        [TestInitialize]
        public void Init()
        {
            mockBlock = new Mock<IBlock>();
        }

        [TestMethod]
        public void FactoryCreatesEmptyShipComponent()
        {
            var factory = new EmptyShipComponentFactory();
            Assert.IsTrue(factory.Create(mockBlock.Object) is EmptyShipComponent);
        }
    }
}
