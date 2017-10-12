using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;

namespace WpfView.Tests
{
    [TestClass]
    public class RectangleFrameworkElementFactoryTest
    {
        [TestMethod]
        public void CheckIfRectangleIsCreateCorrectly()
        {
            var stroke = Brushes.Blue;
            var fill = Brushes.Red;
            var rectangleFactory = new RectangleFrameworkElementFactory(fill, stroke);
            var rectangle = (System.Windows.Shapes.Rectangle)rectangleFactory.CreateElement();

            Assert.AreEqual(stroke, rectangle.Stroke);
            Assert.AreEqual(fill, rectangle.Fill);
            Assert.AreEqual(1, rectangle.StrokeThickness);
        }
    }
}
