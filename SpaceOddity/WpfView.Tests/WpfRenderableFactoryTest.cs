using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows.Controls;

namespace WpfView.Tests
{
    [TestClass]
    public class WpfRenderableFactoryTest
    {
        [TestMethod]
        public void RenderableIsCreatedAddedToCanvas()
        {
            var mockElementFactory = new Mock<IFrameworkElementFactory>();
            var stubElement = new System.Windows.Shapes.Rectangle();
            mockElementFactory.Setup(factory => factory.CreateElement()).Returns(stubElement);
            var canvas = new Canvas();

            var wpfRenderableFactory = new WpfRenderableFactory(canvas, mockElementFactory.Object);
            wpfRenderableFactory.CreateRenderable();

            Assert.IsTrue(canvas.Children.Contains(stubElement));
        }

        [TestMethod]
        public void DestroyingARenderableRemovesFrameworkElementFromCanvas()
        {
            var mockElementFactory = new Mock<IFrameworkElementFactory>();
            var stubElement = new System.Windows.Shapes.Rectangle();
            mockElementFactory.Setup(factory => factory.CreateElement()).Returns(stubElement);
            var canvas = new Canvas();

            var wpfRenderableFactory = new WpfRenderableFactory(canvas, mockElementFactory.Object);

            var renderable = wpfRenderableFactory.CreateRenderable();
            renderable.Delete();

            Assert.IsFalse(canvas.Children.Contains(stubElement));
        }
    }
}
