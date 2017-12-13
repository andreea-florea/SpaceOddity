using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows;
using Geometry;
using System.Windows.Controls;

namespace WpfView.Tests
{
    [TestClass]
    public class GridParentFrameworkElementFactoryTest
    {
        private Mock<IFrameworkElementFactory> mockFrameworkElementFactory;
        private GridParentFrameworkElementFactory gridParentFrameworkElementFactory;
        private FrameworkElement stubElement;
        private Mock<IFrameworkElementWrapper> mockElementWrapper;

        [TestInitialize]
        public void Initialize()
        {
            mockFrameworkElementFactory = new Mock<IFrameworkElementFactory>();
            stubElement = new System.Windows.Shapes.Rectangle();
            var mockElementWrapper = new Mock<IFrameworkElementWrapper>();
            mockElementWrapper.SetupGet(wrapper => wrapper.Element).Returns(stubElement);
            mockFrameworkElementFactory.Setup(factory => factory.CreateElement()).Returns(mockElementWrapper.Object);

            gridParentFrameworkElementFactory =
                new GridParentFrameworkElementFactory(mockFrameworkElementFactory.Object, new Vector2(4.2, 5));
        }

        [TestMethod]
        public void BasicFrameworkElementCalledToCreateFrameworkElement()
        {
            gridParentFrameworkElementFactory.CreateElement();

            mockFrameworkElementFactory.Verify(factory => factory.CreateElement(), Times.Once());
        }

        [TestMethod]
        public void BasicFrameworkElementIsContainedByParentElement()
        {
            var element = gridParentFrameworkElementFactory.CreateElement();

            Assert.IsTrue((element.Element as Grid).Children.Contains(stubElement));
        }

        [TestMethod]
        public void BasicFrameworkElementIsSetToDefaultScale()
        {
            gridParentFrameworkElementFactory.CreateElement();

            Assert.AreEqual(5, stubElement.Height);
            Assert.AreEqual(4.2, stubElement.Width);
        }
    }
}
