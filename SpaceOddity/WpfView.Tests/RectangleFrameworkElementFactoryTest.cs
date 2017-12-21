using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace WpfView.Tests
{
    [TestClass]
    public class RectangleFrameworkElementFactoryTest
    {
        [TestMethod]
        public void CheckIfRectangleIsCreateCorrectly()
        {
            var rectangleFactory = new RectangleFrameworkElementFactory(3);
            var rectangle = rectangleFactory.Create();

            Assert.AreEqual(1, ((Rectangle)rectangle.Element).StrokeThickness);
            Assert.AreEqual(3, Canvas.GetZIndex(rectangle.Element));
        }
    }
}
