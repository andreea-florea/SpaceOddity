using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Interfaces;
using Moq;
using Geometry;

namespace ViewModel.Tests
{
    [TestClass]
    public class AspectRatioRectangleSectionTest
    {
        [TestMethod]
        public void CheckIfAspectRatioCornersAreCalculatedCorrectly()
        {
            var mockSection = new Mock<IRectangleSection>();
            mockSection.Setup(section => section.Section).Returns(new Rectangle(new Vector2(5, 1), new Vector2(13, 5)));
            var rectangleSection = new AspectRatioRectangleSection(new Vector2(1, 2), mockSection.Object);

            Assert.AreEqual(8, rectangleSection.Section.TopLeftCorner.X);
            Assert.AreEqual(1, rectangleSection.Section.TopLeftCorner.Y);
            Assert.AreEqual(10, rectangleSection.Section.BottomRightCorner.X);
            Assert.AreEqual(5, rectangleSection.Section.BottomRightCorner.Y);
        }
    }
}
