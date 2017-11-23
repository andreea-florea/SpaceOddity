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

        [TestInitialize]
        public void Initialize()
        {
            stubElement = new System.Windows.Shapes.Rectangle();
            mockFrameworkElementFactory = new Mock<IFrameworkElementFactory>();
            mockFrameworkElementFactory.Setup(factory => factory.CreateElement()).Returns(stubElement);

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

            Assert.IsTrue((element as Grid).Children.Contains(stubElement));
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
