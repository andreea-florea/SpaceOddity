using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using System.Windows;
using Geometry;

namespace WpfView.Tests
{
    [TestClass]
    public class WpfRenderableTest
    {
        private FrameworkElement element;
        private Canvas canvas;
        private WpfRenderable wpfRenderable;

        [TestInitialize]
        public void Initialize()
        {
            canvas = new Canvas();
            element = new System.Windows.Shapes.Rectangle();
            canvas.Children.Add(element);

            wpfRenderable = new WpfRenderable(element, canvas);
        }

        [TestMethod]
        public void ElementPositionIsUpdatedCorrectly()
        {
            wpfRenderable.Update(new Vector2(6, 5), new Vector2(), new Vector2(2, 4));
            Assert.AreEqual(5, Canvas.GetLeft(element));
            Assert.AreEqual(3, Canvas.GetTop(element));
        }

        [TestMethod]
        public void ElementScaleIsUpdatedCorrectly()
        {
            wpfRenderable.Update(new Vector2(6, 5), new Vector2(), new Vector2(2, 4));
            Assert.AreEqual(4, element.Height);
            Assert.AreEqual(2, element.Width);
        }

        [TestMethod]
        public void DeletingWpfRenderableRemovesElementFromCanvas()
        {
            wpfRenderable.Delete();
            Assert.IsFalse(canvas.Children.Contains(element));
        }
    }
}
