using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Shapes;

namespace WpfView.Tests
{
    [TestClass]
    public class RectangleFrameworkElementWrapperTest
    {
        [TestMethod]
        public void RectangleFrameworkElementWrapperSetsBaseElementCorrectly()
        {
            var rectangle = new Rectangle();
            var rectangleFrameworkElementWrapper = new RectangleFrameworkElementWrapper(rectangle);
            Assert.AreEqual(rectangle, rectangleFrameworkElementWrapper.Element);
        }
    }
}
