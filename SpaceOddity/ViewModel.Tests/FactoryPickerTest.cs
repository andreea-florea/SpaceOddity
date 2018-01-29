using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Algorithms;
using Moq;

namespace ViewModel.Tests
{
    [TestClass]
    public class FactoryPickerTest
    {
        [TestMethod]
        public void FactoryPickerSelectsFactoriesBasedOnKeys()
        {
            var factories = new Dictionary<int, IFactory<double>>();
            var mockFactory1 = new Mock<IFactory<double>>();
            factories.Add(1, mockFactory1.Object);
            var mockFactory2 = new Mock<IFactory<double>>();
            factories.Add(2, mockFactory2.Object);
            mockFactory2.Setup(factory => factory.Create()).Returns(6);

            var picker = new FactoryPicker<int, double>(factories);

            var mockObject = picker.Create(2);
            Assert.AreEqual(6, mockObject);
        }
    }
}
