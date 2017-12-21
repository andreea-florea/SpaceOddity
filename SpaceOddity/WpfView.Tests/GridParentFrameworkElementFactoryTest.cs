using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows;
using Geometry;
using System.Windows.Controls;
using Algorithm;

namespace WpfView.Tests
{
    [TestClass]
    public class GridParentFrameworkElementFactoryTest
    {
        private Mock<IFactory<IFrameworkElementWrapper>> mockFrameworkElementFactory;
        private GridParentFrameworkElementFactory gridParentFrameworkElementFactory;
        private FrameworkElement stubElement;

        [TestInitialize]
        public void Initialize()
        {
            mockFrameworkElementFactory = new Mock<IFactory<IFrameworkElementWrapper>>();
            stubElement = new System.Windows.Shapes.Rectangle();
            var mockElementWrapper = new Mock<IFrameworkElementWrapper>();
            mockElementWrapper.SetupGet(wrapper => wrapper.Element).Returns(stubElement);
            mockFrameworkElementFactory.Setup(factory => factory.Create()).Returns(mockElementWrapper.Object);

            gridParentFrameworkElementFactory =
                new GridParentFrameworkElementFactory(mockFrameworkElementFactory.Object, new Vector2(4.2, 5), 2);
        }

        [TestMethod]
        public void BasicFrameworkElementCalledToCreateFrameworkElement()
        {
            gridParentFrameworkElementFactory.Create();

            mockFrameworkElementFactory.Verify(factory => factory.Create(), Times.Once());
        }

        [TestMethod]
        public void BasicFrameworkElementIsContainedByParentElement()
        {
            var element = gridParentFrameworkElementFactory.Create();

            Assert.IsTrue((element.Element as Grid).Children.Contains(stubElement));
        }

        [TestMethod]
        public void BasicFrameworkElementIsSetToDefaultScale()
        {
            gridParentFrameworkElementFactory.Create();

            Assert.AreEqual(5, stubElement.Height);
            Assert.AreEqual(4.2, stubElement.Width);
        }
    }
}
