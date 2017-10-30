using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Shapes;
using Geometry;
using System.Windows.Controls;
using Moq;
using ViewInterface;

namespace WpfView.Tests
{
    [TestClass]
    public class WpfWorldObjectTest
    {
        [TestMethod]
        public void CheckIfWpfWorldObjectPositionIsSetCorrectly()
        {
            var canvas = new Canvas();
            var frameworkElement = new System.Windows.Shapes.Rectangle();
            canvas.Children.Add(frameworkElement);

            var wpfWorldObject = new WpfWorldObject(frameworkElement, canvas, new Vector2(6, 5), new Vector2(2, 4), null, null);

            Assert.AreEqual(5, Canvas.GetLeft(frameworkElement));
            Assert.AreEqual(3, Canvas.GetTop(frameworkElement));
        }

        [TestMethod]
        public void CheckIfWpfWorldObjectIsScalledCorrectly()
        {
            var canvas = new Canvas();
            var frameworkElement = new System.Windows.Shapes.Rectangle();
            canvas.Children.Add(frameworkElement);

            var wpfWorldObject = new WpfWorldObject(frameworkElement, canvas, new Vector2(6, 5), new Vector2(2, 4), null, null);

            Assert.AreEqual(2, frameworkElement.Width);
            Assert.AreEqual(4, frameworkElement.Height);
        }

        [TestMethod]
        public void CheckIfWpfWorldObjectCanBePositioned()
        {
            var canvas = new Canvas();
            var frameworkElement = new System.Windows.Shapes.Rectangle();
            canvas.Children.Add(frameworkElement);

            var wpfWorldObject = new WpfWorldObject(frameworkElement, canvas, new Vector2(6, 5), new Vector2(2, 4), null, null);
            wpfWorldObject.Position = new Vector2(0, -1);

            Assert.AreEqual(-1, Canvas.GetLeft(frameworkElement));
            Assert.AreEqual(-3, Canvas.GetTop(frameworkElement));
        }

        [TestMethod]
        public void CheckIfWpfWorldObjectCanBeScalled()
        {
            var canvas = new Canvas();
            var frameworkElement = new System.Windows.Shapes.Rectangle();
            canvas.Children.Add(frameworkElement);

            var wpfWorldObject = new WpfWorldObject(frameworkElement, canvas, new Vector2(6, 5), new Vector2(2, 4), null, null);

            wpfWorldObject.Scale = new Vector2(1, 3);

            Assert.AreEqual(1, frameworkElement.Width);
            Assert.AreEqual(3, frameworkElement.Height);
        }

        [TestMethod]
        public void CheckIfLeftClickActionIsAssignedCorrectly()
        {
            var canvas = new Canvas();
            var frameworkElement = new System.Windows.Shapes.Rectangle();
            canvas.Children.Add(frameworkElement);
            var mockAction = new Mock<IAction>();

            var wpfWorldObject =
                new WpfWorldObject(frameworkElement, canvas, new Vector2(6, 5), new Vector2(2, 4), mockAction.Object, null);

            Assert.AreEqual(mockAction.Object, wpfWorldObject.LeftClickAction);
        }

        [TestMethod]
        public void CheckIfRightClickActionIsAssignedCorrectly()
        {
            var canvas = new Canvas();
            var frameworkElement = new System.Windows.Shapes.Rectangle();
            canvas.Children.Add(frameworkElement);
            var mockAction = new Mock<IAction>();

            var wpfWorldObject =
                new WpfWorldObject(frameworkElement, canvas, new Vector2(6, 5), new Vector2(2, 4), null, mockAction.Object);

            Assert.AreEqual(mockAction.Object, wpfWorldObject.RightClickAction);
        }

        [TestMethod]
        public void CheckIfWpfObjectIsRemovedFromTheCanvas()
        {
            var canvas = new Canvas();
            var frameworkElement = new System.Windows.Shapes.Rectangle();
            canvas.Children.Add(frameworkElement);

            var wpfWorldObject =
                new WpfWorldObject(frameworkElement, canvas, new Vector2(), new Vector2(), null, null);
            wpfWorldObject.Delete();

            Assert.IsFalse(canvas.Children.Contains(frameworkElement));
        }
    }
}
