using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturalNumbersMath;

namespace Geometry.Tests
{
    [TestClass]
    public class CoordinateExtensionsTest
    {
        [TestMethod]
        public void ConvertsCorrectlyToVector2()
        {
            var coordinate = new Coordinate(3, 5);
            var convertedVector = coordinate.ToVector2();
            Assert.AreEqual(3, convertedVector.X);
            Assert.AreEqual(5, convertedVector.Y);
        }
    }
}
