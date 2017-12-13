using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows;
using System.Windows.Controls;
using Geometry;

namespace WpfView.Tests
{
    [TestClass]
    public class FixedSizeFrameworkElementFactoryTest
    {
        private Mock<IFrameworkElementFactory> mockFrameworkElementFactory;
        private FixedSizeFrameworkElementFactory fixedSizeFrameworkElementFactory;
        private Mock<IFrameworkElementWrapper> mockElementWrapper;
        private FrameworkElement stubElement;
 
        [TestInitialize]
        public void Initialize()
        {
            stubElement = new System.Windows.Shapes.Rectangle();
            mockElementWrapper = new Mock<IFrameworkElementWrapper>();
            mockElementWrapper.SetupGet(wrapper => wrapper.Element).Returns(stubElement);
            mockFrameworkElementFactory = new Mock<IFrameworkElementFactory>();
            mockFrameworkElementFactory.Setup(factory => factory.CreateElement()).Returns(mockElementWrapper.Object);

            fixedSizeFrameworkElementFactory =
                new FixedSizeFrameworkElementFactory(mockFrameworkElementFactory.Object, new Vector2(3.5, 2));
        }

        [TestMethod]
        public void BasicFrameworkElementCalledToCreateFrameworkElement()
        {
            fixedSizeFrameworkElementFactory.CreateElement();

            mockFrameworkElementFactory.Verify(factory => factory.CreateElement(), Times.Once());
        }

        [TestMethod]
        public void BasicFrameworkElementIsContainedByParentElement()
        {
            var element = fixedSizeFrameworkElementFactory.CreateElement();

            Assert.IsTrue((element.Element as Canvas).Children.Contains(stubElement));
        }

        [TestMethod]
        public void BasicFrameworkElementIsSetToDefaultScale()
        {
            fixedSizeFrameworkElementFactory.CreateElement();

            Assert.AreEqual(2, stubElement.Height);
            Assert.AreEqual(3.5, stubElement.Width);
        }

        [TestMethod]
        public void BasicFrameworkElementIsPositionedInCenter()
        {
            fixedSizeFrameworkElementFactory.CreateElement();

            Assert.AreEqual(-1.75, Canvas.GetLeft(stubElement));
            Assert.AreEqual(-1, Canvas.GetTop(stubElement));
        }
    }
}
