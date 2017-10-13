using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using Moq;
using ViewInterface;

namespace WpfView.Tests
{
    [TestClass]
    public class WpfWorldObjectFactoryTest
    {
        [TestMethod]
        public void WorldObjectCreatedByFactoyAreAddedToCanvas()
        {
            var canvas = new Canvas();

            var element = new System.Windows.Shapes.Rectangle();
            var mockFrameworkElementFactory = new Mock<IFrameworkElementFactory>();
            mockFrameworkElementFactory.Setup(factory => factory.CreateElement()).Returns(element);
            
            var worldObjectFactory = new WpfWorldObjectFactory(canvas, mockFrameworkElementFactory.Object);
            worldObjectFactory.CreateObject();

            Assert.IsTrue(canvas.Children.Contains(element));
        }

        [TestMethod]
        public void WorldObjectCreatedByFactoyHaveNoDefaultActions()
        {
            var canvas = new Canvas();

            var element = new System.Windows.Shapes.Rectangle();
            var mockFrameworkElementFactory = new Mock<IFrameworkElementFactory>();
            mockFrameworkElementFactory.Setup(factory => factory.CreateElement()).Returns(element);

            var worldObjectFactory = new WpfWorldObjectFactory(canvas, mockFrameworkElementFactory.Object);
            var worldObject = worldObjectFactory.CreateObject();

            Assert.IsTrue(worldObject.LeftClickAction is NoAction);
        }
    }
}
