using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using Moq;
using System.Collections.Generic;

namespace Algorithm.Tests
{
    [TestClass]
    public class MultipleFactoryTest
    {
        [TestMethod]
        public void MultipleFactoryUsesCorrectBaseFactory()
        {
            var factories = new List<Mock<IFactory<int>>>();
            factories.Add(new Mock<IFactory<int>>());
            factories.Add(new Mock<IFactory<int>>());
            factories.Add(new Mock<IFactory<int>>());

            factories[1].Setup(factory => factory.Create()).Returns(7);
            factories[2].Setup(factory => factory.Create()).Returns(5);

            var multipleFactories = new MultipleFactory<int>(factories.Select(factory => factory.Object).ToArray(), 1);

            Assert.AreEqual(7, multipleFactories.Create());
            multipleFactories.SelectedIndex = 2;
            Assert.AreEqual(5, multipleFactories.Create());
        }
    }
}
