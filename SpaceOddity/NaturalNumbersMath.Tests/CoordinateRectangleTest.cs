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
            var bottomLeftCorner = new Coordinate(0, 1);
            var topRightCorner = new Coordinate(2, 3);

            var rectangle = new CoordinateRectangle(bottomLeftCorner, topRightCorner);
            Assert.AreEqual(bottomLeftCorner, rectangle.bottomLeftCorner);
            Assert.AreEqual(topRightCorner, rectangle.topRightCorner);
        }

        [TestMethod]
        public void CheckIfCorrectCoordinatesAreIteratedOver()
        {
            var bottomLeftCorner = new Coordinate(0, 1);
            var topRightCorner = new Coordinate(2, 3);

            var rectangle = new CoordinateRectangle(bottomLeftCorner, topRightCorner);
            var points = rectangle.Points.ToArray();

            Assert.AreEqual(new Coordinate(0, 1), points[0]);
            Assert.AreEqual(new Coordinate(1, 1), points[1]);
            Assert.AreEqual(new Coordinate(0, 2), points[2]);
            Assert.AreEqual(new Coordinate(1, 2), points[3]);
        }
    }
}
