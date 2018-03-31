using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.MenuControls;
using System.Collections.Generic;
using Algorithms;
using Moq;

namespace ViewModel.Tests.MenuControls
{
    [TestClass]
    public class DropDownListFactoryTest
    {
        private Mock<IWorldObject> mockItem;
        private DropDownListFactory factory;

        [TestInitialize]
        public void Initialize()
        {
            var itemFactories = new List<IFactory<IWorldObject>>();
            var mockItemFactory = new Mock<IFactory<IWorldObject>>();
            mockItem = new Mock<IWorldObject>();
            mockItemFactory.Setup(itemFactory => itemFactory.Create()).Returns(mockItem.Object);
            itemFactories.Add(new Mock<IFactory<IWorldObject>>().Object);
            itemFactories.Add(mockItemFactory.Object);

            factory = new DropDownListFactory(itemFactories, 1);
        }

        [TestMethod]
        public void DropDownListCreatedCorrectly()
        {
            var dropDown = factory.Create();

            Assert.AreEqual(mockItem.Object, dropDown.Object);
            Assert.AreEqual(1, dropDown.SelectedItem);
        }
    }
}
