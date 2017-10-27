using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalNumbersMath.Tests
{
    [TestClass]
    public class CoordinateTest
    {
        [TestMethod]
        public void CoordinateValuesAreSetCorrecly()
        {
            var coordinate = new Coordinate(2, 3);
            Assert.AreEqual(2, coordinate.X);
            Assert.AreEqual(3, coordinate.Y);
        }

        [TestMethod]
        public void CoordinateValuesRotateQuarterCircleRightCorrectly()
        {
            var coordinate = new Coordinate(2, 1);
            var rotatedCoordinate = coordinate.RotateQuarterCircleRight();
            Assert.AreEqual(1, rotatedCoordinate.X);
            Assert.AreEqual(-2, rotatedCoordinate.Y);
        }

        [TestMethod]
        public void CoordinateValuesAddCorrecly()
        {
            var coordinate = new Coordinate(2, 3);
            var other = new Coordinate(3, 1);
            var sum = coordinate + other;
            Assert.AreEqual(5, sum.X);
            Assert.AreEqual(4, sum.Y);
        }

        [TestMethod]
        public void CoordinateValuesSubtractCorrecly()
        {
            var coordinate = new Coordinate(2, 3);
            var other = new Coordinate(3, 1);
            var difference = coordinate - other;
            Assert.AreEqual(-1, difference.X);
            Assert.AreEqual(2, difference.Y);
        }
    }
}
