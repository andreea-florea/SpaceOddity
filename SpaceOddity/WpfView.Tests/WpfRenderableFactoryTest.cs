using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfView.Tests
{
    [TestClass]
    public class WpfRenderableFactoryTest
    {
        private Canvas canvas;
        private Mock<IFrameworkElementFactory> mockElementFactory;
        private System.Windows.Shapes.Rectangle stubElement;
        private Mock<IFrameworkElementWrapper> mockElementWrapper;
        private BuilderWorldObjectState[] states;
        private WpfRenderableFactory wpfRenderableFactory;

        [TestInitialize]
        public void Initialize()
        {
            canvas = new Canvas();
            mockElementFactory = new Mock<IFrameworkElementFactory>();
            stubElement = new System.Windows.Shapes.Rectangle();
            mockElementWrapper = new Mock<IFrameworkElementWrapper>();
            states = new BuilderWorldObjectState[2];
            mockElementWrapper.SetupGet(wrapper => wrapper.Element).Returns(stubElement);
            mockElementFactory.Setup(factory => factory.CreateElement()).Returns(mockElementWrapper.Object);
            
            wpfRenderableFactory = new WpfRenderableFactory(canvas, mockElementFactory.Object, states);

        }

        [TestMethod]
        public void RenderableIsCreatedAddedToCanvas()
        {
            wpfRenderableFactory.CreateRenderable();
            Assert.IsTrue(canvas.Children.Contains(stubElement));
        }

        [TestMethod]
        public void RenderableIsCreatedAndInitializedWithFirstState()
        {
            var red = new ColorVector(1.0, 0.0, 0.0);
            var green = new ColorVector(0.0, 1.0, 0.0);
            states[0] = new BuilderWorldObjectState(green, red);
            mockElementWrapper.SetupSet(wrapper => wrapper.Border = red).Verifiable();
            mockElementWrapper.SetupSet(wrapper => wrapper.Fill = green).Verifiable();
            
            wpfRenderableFactory.CreateRenderable();

            mockElementWrapper.VerifySet(wrapper => wrapper.Border = red, Times.Once());
            mockElementWrapper.VerifySet(wrapper => wrapper.Fill = green, Times.Once());
        }

        [TestMethod]
        public void DestroyingARenderableRemovesFrameworkElementFromCanvas()
        {
            var renderable = wpfRenderableFactory.CreateRenderable();
            renderable.Delete();

            Assert.IsFalse(canvas.Children.Contains(stubElement));
        }
    }
}
