using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Geometry;

namespace ViewModel.Tests.RectangleSections
{
    [TestClass]
    public class AspectRatioRectangleSectionTest
    {
        [TestMethod]
        public void CheckIfAspectRatioCornersAreCalculatedCorrectlyForRectanglesWithLongWidths()
        {
            var mockSection = new Mock<IRectangleSection>();
            mockSection.Setup(section => section.Section).Returns(new Rectangle(new Vector2(5, 1), new Vector2(13, 5)));
            var rectangleSection = new AspectRatioRectangleSection(new Vector2(1, 2), mockSection.Object);

            Assert.AreEqual(8, rectangleSection.Section.BottomLeftCorner.X);
            Assert.AreEqual(1, rectangleSection.Section.BottomLeftCorner.Y);
            Assert.AreEqual(10, rectangleSection.Section.TopRightCorner.X);
            Assert.AreEqual(5, rectangleSection.Section.TopRightCorner.Y);
        }

        [TestMethod]
        public void CheckIfAspectRatioCornersAreCalculatedCorrectlyForRectanglesWithLongHeights()
        {
            var mockSection = new Mock<IRectangleSection>();
            mockSection.Setup(section => section.Section).Returns(new Rectangle(new Vector2(1, 5), new Vector2(5, 13)));
            var rectangleSection = new AspectRatioRectangleSection(new Vector2(2, 1), mockSection.Object);

            Assert.AreEqual(1, rectangleSection.Section.BottomLeftCorner.X);
            Assert.AreEqual(8, rectangleSection.Section.BottomLeftCorner.Y);
            Assert.AreEqual(5, rectangleSection.Section.TopRightCorner.X);
            Assert.AreEqual(10, rectangleSection.Section.TopRightCorner.Y);
        }
    }
}
