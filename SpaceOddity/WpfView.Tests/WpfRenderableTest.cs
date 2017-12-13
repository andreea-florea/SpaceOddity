using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using System.Windows;
using Geometry;
using Moq;

namespace WpfView.Tests
{
    [TestClass]
    public class WpfRenderableTest
    {
        private FrameworkElement stubElement;
        private Mock<IFrameworkElementWrapper> mockElementWrapper;
        private BuilderWorldObjectState[] stubStates;
        private Canvas canvas;
        private WpfRenderable wpfRenderable;

        [TestInitialize]
        public void Initialize()
        {
            canvas = new Canvas();
            stubElement = new System.Windows.Shapes.Rectangle();
            mockElementWrapper = new Mock<IFrameworkElementWrapper>();
            mockElementWrapper.SetupGet(wrapper => wrapper.Element).Returns(stubElement);
            stubStates = new BuilderWorldObjectState[2];
            canvas.Children.Add(stubElement);

            wpfRenderable = new WpfRenderable(mockElementWrapper.Object, canvas, stubStates);
        }

        [TestMethod]
        public void ElementPositionIsUpdatedCorrectly()
        {
            wpfRenderable.Update(new Vector2(6, 5), new Vector2(), new Vector2(2, 4));
            Assert.AreEqual(5, Canvas.GetLeft(stubElement));
            Assert.AreEqual(3, Canvas.GetTop(stubElement));
        }

        [TestMethod]
        public void ElementScaleIsUpdatedCorrectly()
        {
            wpfRenderable.Update(new Vector2(6, 5), new Vector2(), new Vector2(2, 4));
            Assert.AreEqual(4, stubElement.Height);
            Assert.AreEqual(2, stubElement.Width);
        }

        [TestMethod]
        public void DeletingWpfRenderableRemovesElementFromCanvas()
        {
            wpfRenderable.Delete();
            Assert.IsFalse(canvas.Children.Contains(stubElement));
        }

        [TestMethod]
        public void WpfStateIsSetCorrectly()
        {
            var fill = new ColorVector(1, 0.5, 0.1);
            var border = new ColorVector(0.1, 0.2, 0.3);
            var stubState = new BuilderWorldObjectState(fill, border);
            mockElementWrapper.SetupSet(wrapper => wrapper.Fill = fill).Verifiable();
            mockElementWrapper.SetupSet(wrapper => wrapper.Fill = fill).Verifiable();
            stubStates[1] = stubState;

            wpfRenderable.SetState(1);
            
            mockElementWrapper.VerifySet(wrapper => wrapper.Fill = fill, Times.Once());
            mockElementWrapper.VerifySet(wrapper => wrapper.Border = border, Times.Once());
        }
    }
}
