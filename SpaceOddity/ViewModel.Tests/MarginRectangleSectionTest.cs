using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel.Interfaces;
using Geometry;

namespace ViewModel.Tests
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

            Assert.AreEqual(2, rectangleSection.Section.TopLeftCorner.X);
            Assert.AreEqual(3, rectangleSection.Section.TopLeftCorner.Y);
            Assert.AreEqual(3, rectangleSection.Section.BottomRightCorner.X);
            Assert.AreEqual(5, rectangleSection.Section.BottomRightCorner.Y);
        }
    }
}
