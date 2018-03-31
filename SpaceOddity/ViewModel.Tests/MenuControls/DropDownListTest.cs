using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.MenuControls;
using Moq;
using Algorithms;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel.Tests.MenuControls
{
    [TestClass]
    public class DropDownListTest
    {
        private DropDownList dropDownList;
        private Mock<IFactory<IWorldObject>> mockFactory1;
        private Mock<IFactory<IWorldObject>> mockFactory2;
        private Mock<IWorldObject> mockItem1;
        private Mock<IWorldObject> mockItem2;

        [TestInitialize]
        public void Initialize()
        {
            mockFactory1 = new Mock<IFactory<IWorldObject>>();
            mockItem1 = new Mock<IWorldObject>();
            mockFactory1.Setup(factory => factory.Create()).Returns(mockItem1.Object);
            mockFactory2 = new Mock<IFactory<IWorldObject>>();
            mockItem2 = new Mock<IWorldObject>();
            mockFactory2.Setup(factory => factory.Create()).Returns(mockItem2.Object);

            var itemFactories = new List<IFactory<IWorldObject>>();
            itemFactories.Add(mockFactory1.Object);
            itemFactories.Add(mockFactory2.Object);

            var mockItem = new Mock<IWorldObject>();
            dropDownList = new DropDownList(0, mockItem.Object, itemFactories);
        }

        [TestMethod]
        public void DropDownListCanBeToggled()
        {
            Assert.AreEqual(0, dropDownList.ExpandedItems.Count());
            dropDownList.Toggle();
            Assert.AreEqual(mockItem1.Object, dropDownList.ExpandedItems[0]);
            Assert.AreEqual(mockItem2.Object, dropDownList.ExpandedItems[1]);
            Assert.AreEqual(2, dropDownList.ExpandedItems.Count());
            dropDownList.Toggle();
            mockItem1.Verify(item => item.Delete(), Times.Once());
            mockItem2.Verify(item => item.Delete(), Times.Once());
            Assert.AreEqual(0, dropDownList.ExpandedItems.Count());
        }
    }
}
