using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturalNumbersMath;
using ViewModel.DataStructures;

namespace ViewModel.Tests.DataStructures
{
    [TestClass]
    public class CoordinatePairTest
    {
        [TestMethod]
        public void CoordinatePairIsCreatedCorrectly()
        {
            var pair = new CoordinatePair(new Coordinate(3, 2), new Coordinate(3, 1));

            Assert.AreEqual(new Coordinate(3, 2), pair.First);
            Assert.AreEqual(new Coordinate(3, 1), pair.Second);
        }

        [TestMethod]
        public void CommonPositionIsFoundCorrectly()
        {
            var pair1 = new CoordinatePair(new Coordinate(3, 2), new Coordinate(3, 1));
            var pair2 = new CoordinatePair(new Coordinate(3, 2), new Coordinate(2, 2));
            Assert.AreEqual(new Coordinate(3, 2), pair1.GetCommonPosition(pair2));
        }
    }
}
