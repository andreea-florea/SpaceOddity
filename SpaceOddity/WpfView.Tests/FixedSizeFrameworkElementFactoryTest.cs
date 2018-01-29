using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows;
using System.Windows.Controls;
using Geometry;
using Algorithms;

namespace WpfView.Tests
{
    [TestClass]
    public class FixedSizeFrameworkElementFactoryTest
    {
        private Mock<IFactory<IFrameworkElementWrapper>> mockFrameworkElementFactory;
        private FixedSizeFrameworkElementFactory fixedSizeFrameworkElementFactory;
        private Mock<IFrameworkElementWrapper> mockElementWrapper;
        private FrameworkElement stubElement;
 
        [TestInitialize]
        public void Initialize()
        {
            stubElement = new System.Windows.Shapes.Rectangle();
            mockElementWrapper = new Mock<IFrameworkElementWrapper>();
            mockElementWrapper.SetupGet(wrapper => wrapper.Element).Returns(stubElement);
            mockFrameworkElementFactory = new Mock<IFactory<IFrameworkElementWrapper>>();
            mockFrameworkElementFactory.Setup(factory => factory.Create()).Returns(mockElementWrapper.Object);

            fixedSizeFrameworkElementFactory =
                new FixedSizeFrameworkElementFactory(mockFrameworkElementFactory.Object, new Vector2(3.5, 2), 7);
        }

        [TestMethod]
        public void BasicFrameworkElementCalledToCreateFrameworkElement()
        {
            fixedSizeFrameworkElementFactory.Create();

            mockFrameworkElementFactory.Verify(factory => factory.Create(), Times.Once());
        }

        [TestMethod]
        public void BasicFrameworkElementIsContainedByParentElement()
        {
            var element = fixedSizeFrameworkElementFactory.Create();

            Assert.IsTrue((element.Element as Canvas).Children.Contains(stubElement));
        }

        [TestMethod]
        public void BasicFrameworkElementIsSetToDefaultScale()
        {
            fixedSizeFrameworkElementFactory.Create();

            Assert.AreEqual(2, stubElement.Height);
            Assert.AreEqual(3.5, stubElement.Width);
        }

        [TestMethod]
        public void BasicFrameworkElementIsPositionedInCenter()
        {
            fixedSizeFrameworkElementFactory.Create();

            Assert.AreEqual(-1.75, Canvas.GetLeft(stubElement));
            Assert.AreEqual(-1, Canvas.GetTop(stubElement));
        }
    }
}
