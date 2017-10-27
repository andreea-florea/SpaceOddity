using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalNumbersMath.Tests
{
    [TestClass]
    public class CoordinatesTest
    {
        [TestMethod]
        public void CoordinateDirectionsAreCorrect()
        {
            Assert.AreEqual(0, Coordinates.Zero.X);
            Assert.AreEqual(0, Coordinates.Zero.Y);
            Assert.AreEqual(0, Coordinates.Up.X);
            Assert.AreEqual(1, Coordinates.Up.Y);
            Assert.AreEqual(1, Coordinates.Right.X);
            Assert.AreEqual(0, Coordinates.Right.Y);
            Assert.AreEqual(-1, Coordinates.Left.X);
            Assert.AreEqual(0, Coordinates.Left.Y);
            Assert.AreEqual(0, Coordinates.Down.X);
            Assert.AreEqual(-1, Coordinates.Down.Y);

            Assert.AreEqual(Coordinates.Up.X, Coordinates.Left.RotateQuarterCircleRight().X);
            Assert.AreEqual(Coordinates.Up.Y, Coordinates.Left.RotateQuarterCircleRight().Y);
        }

        [TestMethod]
        public void CoordinateDirectionsListAreCorrect()
        {
            Assert.AreEqual(Coordinates.Up, Coordinates.Directions[0]);
            Assert.AreEqual(Coordinates.Right, Coordinates.Directions[1]);
            Assert.AreEqual(Coordinates.Down, Coordinates.Directions[2]);
            Assert.AreEqual(Coordinates.Left, Coordinates.Directions[3]);
        }
    }
}
