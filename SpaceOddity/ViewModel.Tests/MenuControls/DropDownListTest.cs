using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.MenuControls;
using Moq;
using Algorithms;
using System.Collections.Generic;
using System.Linq;
using Geometry;
using ViewInterface;

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

            var direction = new Vector2(3, 0);
            dropDownList = new DropDownList(0, new Vector2(1, 1), new Vector2(3, 3), direction, itemFactories);
        }

        [TestMethod]
        public void DropDownListCanBeToggled()
        {
            dropDownList.Toggle();
            mockFactory1.Verify(factory => factory.Create(), Times.Exactly(2));
            mockFactory2.Verify(factory => factory.Create(), Times.Once());
            dropDownList.Toggle();
            mockItem1.Verify(item => item.Delete(), Times.Once());
            mockItem2.Verify(item => item.Delete(), Times.Once());
        }

        [TestMethod]
        public void ElementsAreToggledAtCorrectPosition()
        {
            dropDownList.Toggle();

            mockItem1.VerifySet(item => item.Position = new Vector2(4, 1), Times.Once());
            mockItem2.VerifySet(item => item.Position = new Vector2(7, 1), Times.Once());
            mockItem1.VerifySet(item => item.Scale = new Vector2(3, 3), Times.Exactly(2));
            mockItem2.VerifySet(item => item.Scale = new Vector2(3, 3), Times.Once());
        }

        [TestMethod]
        public void ElementsAreCreatedWithSelectAction()
        {
            IAction setClick1 = null;
            IAction setClick2 = null;
            mockItem1.SetupSet(item => item.LeftClickAction = It.IsAny<IAction>()).Callback<IAction>(click => setClick1 = click);
            mockItem2.SetupSet(item => item.LeftClickAction = It.IsAny<IAction>()).Callback<IAction>(click => setClick2 = click);

            dropDownList.Toggle();

            (setClick2 as SelectDropDownItemAction).Execute();
            Assert.AreEqual(1, dropDownList.SelectedIndex);
            (setClick1 as SelectDropDownItemAction).Execute();
            Assert.AreEqual(0, dropDownList.SelectedIndex);
        }

        [TestMethod]
        public void ViewChangedWithIndex()
        {
            dropDownList.SelectedIndex = 1;
            mockItem1.Verify(dropDown => dropDown.Delete(), Times.Once());
            mockItem2.VerifySet(item => item.Position = new Vector2(1, 1), Times.Once());
            mockItem2.VerifySet(item => item.Scale = new Vector2(3, 3), Times.Once());
            Assert.AreEqual(dropDownList.Object, mockItem2.Object);
        }
    }
}
