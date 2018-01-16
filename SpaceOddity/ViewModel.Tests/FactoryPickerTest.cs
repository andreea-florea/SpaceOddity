using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Algorithm;
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

        [TestMethod]
        public void CanAddFactoryToPicker()
        {
            var factories = new Dictionary<int, IFactory<double>>();
            var picker = new FactoryPicker<int, double>(factories);

            var mockFactory = new Mock<IFactory<double>>();
            mockFactory.Setup(factory => factory.Create()).Returns(7);
            picker.Add(5, mockFactory.Object);

            var mockObject = picker.Create(5);
            Assert.AreEqual(7, mockObject);
        }

        [TestMethod]
        public void CanAddFactoryWithSameKeyOnlyAfterOldOneIsRemoved()
        {
            var factories = new Dictionary<int, IFactory<double>>();
            var mockFactory = new Mock<IFactory<double>>();
            mockFactory.Setup(factory => factory.Create()).Returns(7);
            factories.Add(5, mockFactory.Object);

            var picker = new FactoryPicker<int, double>(factories);
            picker.Remove(5);
            picker.Add(5, mockFactory.Object);

            var mockObject = picker.Create(5);
            Assert.AreEqual(7, mockObject);
        }
    }
}
