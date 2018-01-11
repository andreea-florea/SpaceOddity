using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geometry;
using Moq;

namespace ViewModel.Tests.RectangleSections
{
    [TestClass]
    public class FullRectangleSectionTest
    {
        [TestMethod]
        public void CheckIfRectangleCornersAreReturnedCorrectly()
        {
            var rectangle = new Rectangle(new Vector2(1, 1), new Vector2(4, 7));
            var rectangleSection = new FullRectangleSection(rectangle);

            Assert.AreEqual(1, rectangleSection.Section.BottomLeftCorner.X);
            Assert.AreEqual(1, rectangleSection.Section.BottomLeftCorner.Y);
            Assert.AreEqual(4, rectangleSection.Section.TopRightCorner.X);
            Assert.AreEqual(7, rectangleSection.Section.TopRightCorner.Y);
        }
    }
}
