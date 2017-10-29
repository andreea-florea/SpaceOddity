using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace NaturalNumbersMath.Tests
{
    [TestClass]
    public class CoordinateRectangleTest
    {
        [TestMethod]
        public void CornersAreSetCorrectly()
        {
            var topLeftCorner = new Coordinate(0, 1);
            var bottomRightCorner = new Coordinate(2, 3);

            var rectangle = new CoordinateRectangle(topLeftCorner, bottomRightCorner);
            Assert.AreEqual(topLeftCorner, rectangle.TopLeftCorner);
            Assert.AreEqual(bottomRightCorner, rectangle.BottomRightCorner);
        }

        [TestMethod]
        public void CheckIfCorrectCoordinatesAreIteratedOver()
        {
            var topLeftCorner = new Coordinate(0, 1);
            var bottomRightCorner = new Coordinate(2, 3);

            var rectangle = new CoordinateRectangle(topLeftCorner, bottomRightCorner);
            var points = rectangle.Points.ToArray();

            Assert.AreEqual(new Coordinate(0, 1), points[0]);
            Assert.AreEqual(new Coordinate(1, 1), points[1]);
            Assert.AreEqual(new Coordinate(0, 2), points[2]);
            Assert.AreEqual(new Coordinate(1, 2), points[3]);
        }
    }
}
