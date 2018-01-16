using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Tests
{
    [TestClass]
    public class EmptyShipComponentFactoryTest
    {
        [TestMethod]
        public void FactoryCreatesEmptyShipComponent()
        {
            var factory = new EmptyShipComponentFactory();
            Assert.IsTrue(factory.Create() is EmptyShipComponent);
        }
    }
}
