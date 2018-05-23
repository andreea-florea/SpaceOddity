using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.MenuControls;
using System.Collections.Generic;
using Algorithms;
using Moq;
using Geometry;

namespace ViewModel.Tests.MenuControls
{
    [TestClass]
    public class DropDownListFactoryTest
    {
        private Mock<IWorldObject> mockItem;
        private DropDownListFactory factory;
        private Vector2 position;
        private Vector2 direction;

        [TestInitialize]
        public void Initialize()
        {
            var itemFactories = new List<IFactory<IWorldObject>>();
            var mockItemFactory = new Mock<IFactory<IWorldObject>>();
            mockItem = new Mock<IWorldObject>();
            mockItemFactory.Setup(itemFactory => itemFactory.Create()).Returns(mockItem.Object);
            itemFactories.Add(new Mock<IFactory<IWorldObject>>().Object);
            itemFactories.Add(mockItemFactory.Object);
            position = new Vector2(3, 1);
            direction = new Vector2(4, 5);

            factory = new DropDownListFactory(itemFactories, position, direction, 1);
        }

        [TestMethod]
        public void DropDownListCreatedCorrectly()
        {
            var dropDown = factory.Create();

            Assert.AreEqual(mockItem.Object, dropDown.Object);
            Assert.AreEqual(1, dropDown.SelectedIndex);

            mockItem.SetupAllProperties();
            mockItem.VerifySet(item => item.Position = position, Times.Once());
            Assert.AreEqual(direction, dropDown.Direction);
            mockItem.VerifySet(item => item.Scale = direction.Magnitude * Vector2s.Diagonal, Times.Once());
        }
    }
}
