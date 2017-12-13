using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfView.Tests
{
    [TestClass]
    public class RectangleFrameworkElementFactoryTest
    {
        [TestMethod]
        public void CheckIfRectangleIsCreateCorrectly()
        {
            var rectangleFactory = new RectangleFrameworkElementFactory();
            var rectangle = rectangleFactory.CreateElement();

            Assert.AreEqual(1, ((Rectangle)rectangle.Element).StrokeThickness);
        }
    }
}
