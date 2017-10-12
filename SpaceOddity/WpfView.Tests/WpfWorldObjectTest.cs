using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Shapes;
using Geometry;
using System.Windows.Controls;

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

            var wpfWorldObject = new WpfWorldObject(frameworkElement, new Vector2(6, 5), new Vector2(2, 4));

            Assert.AreEqual(5, Canvas.GetLeft(frameworkElement));
            Assert.AreEqual(3, Canvas.GetTop(frameworkElement));
        }

        [TestMethod]
        public void CheckIfWpfWorldObjectIsScalledCorrectly()
        {
            var canvas = new Canvas();
            var frameworkElement = new System.Windows.Shapes.Rectangle();
            canvas.Children.Add(frameworkElement);

            var wpfWorldObject = new WpfWorldObject(frameworkElement, new Vector2(6, 5), new Vector2(2, 4));

            Assert.AreEqual(2, frameworkElement.Width);
            Assert.AreEqual(4, frameworkElement.Height);
        }

        [TestMethod]
        public void CheckIfWpfWorldObjectCanBePositioned()
        {
            var canvas = new Canvas();
            var frameworkElement = new System.Windows.Shapes.Rectangle();
            canvas.Children.Add(frameworkElement);

            var wpfWorldObject = new WpfWorldObject(frameworkElement, new Vector2(6, 5), new Vector2(2, 4));
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

            var wpfWorldObject = new WpfWorldObject(frameworkElement, new Vector2(6, 5), new Vector2(2, 4));

            wpfWorldObject.Scale = new Vector2(1, 3);

            Assert.AreEqual(1, frameworkElement.Width);
            Assert.AreEqual(3, frameworkElement.Height);
        }
    }
}
