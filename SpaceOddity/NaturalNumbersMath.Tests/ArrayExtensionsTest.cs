using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalNumbersMath.Tests
{
    [TestClass]
    public class ArrayExtensionsTest
    {
        [TestMethod]
        public void CanAccessMemberOfArrayWithExtension()
        {
            int[,] array = new int[3, 4];
            array[2, 1] = 5;
            Assert.AreEqual(5, array.Get(new Coordinate(1, 2)));
        }

        [TestMethod]
        public void CanSetMemberOfArrayWithExtension()
        {
            int[,] array = new int[3, 4];
            array.Set(new Coordinate(1, 2), 5);
            Assert.AreEqual(5, array[2, 1]);
        }

        [TestMethod]
        public void CoordinateWithNegativeXOutsideOfBounds()
        {
            int[,] array = new int[4, 5];
            Assert.IsFalse(array.IsWithinBounds(new Coordinate(-1, 3)));
        }

        [TestMethod]
        public void CoordinateWithNegativeYOutsideOfBounds()
        {
            int[,] array = new int[4, 5];
            Assert.IsFalse(array.IsWithinBounds(new Coordinate(1, -3)));
        }

        [TestMethod]
        public void CoordinateWithOverflowingXOutsideOfBounds()
        {
            int[,] array = new int[4, 5];
            Assert.IsFalse(array.IsWithinBounds(new Coordinate(7, 3)));
        }

        [TestMethod]
        public void CoordinateWithOverflowingYOutsideOfBounds()
        {
            int[,] array = new int[4, 5];
            Assert.IsFalse(array.IsWithinBounds(new Coordinate(3, 7)));
        }

        [TestMethod]
        public void CoordinateIsWithinBounds()
        {
            int[,] array = new int[4, 5];
            Assert.IsTrue(array.IsWithinBounds(new Coordinate(2, 3)));
        }
    }
}
