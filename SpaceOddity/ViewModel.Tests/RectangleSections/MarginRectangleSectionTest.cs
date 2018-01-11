using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Geometry;

namespace ViewModel.Tests.RectangleSections
{
    [TestClass]
    public class MarginRectangleSectionTest
    {
        [TestMethod]
        public void CheckIfMarginCornersAreCalculatedCorrectly()
        {
            var mockSection = new Mock<IRectangleSection>();
            mockSection.Setup(section => section.Section).Returns(new Rectangle(new Vector2(1, 1), new Vector2(4, 7)));
            var rectangleSection = new MarginRectangleSection(new Vector2(1, 2), mockSection.Object);

            Assert.AreEqual(2, rectangleSection.Section.BottomLeftCorner.X);
            Assert.AreEqual(3, rectangleSection.Section.BottomLeftCorner.Y);
            Assert.AreEqual(3, rectangleSection.Section.TopRightCorner.X);
            Assert.AreEqual(5, rectangleSection.Section.TopRightCorner.Y);
        }
    }
}
