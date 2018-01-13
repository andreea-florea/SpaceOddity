using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.DataStructures;

namespace BlueprintBuildingViewModel.Tests.DataStructures
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
            var commonPosition = pair1.GetCommonPosition(pair2);
            Assert.AreEqual(new Coordinate(3, 2),commonPosition.Element);
            Assert.IsTrue(commonPosition.IsFound);
        }

        [TestMethod]
        public void CoordinatePairEqualityChecksCorrectly()
        {
            var pair1 = new CoordinatePair(new Coordinate(3, 2), new Coordinate(2, 2));
            var pair2 = new CoordinatePair(new Coordinate(3, 2), new Coordinate(2, 2));
            var pair3 = new CoordinatePair(new Coordinate(3, 2), new Coordinate(2, 3));
            var pair4 = new CoordinatePair(new Coordinate(1, 2), new Coordinate(2, 2));

            Assert.IsTrue(pair1 == pair2);
            Assert.IsFalse(pair1 == pair3);
            Assert.IsFalse(pair1 == pair4);
        }

        [TestMethod]
        public void CoordinatePairsAreEqualWithInterchanglePositions()
        {
            var pair1 = new CoordinatePair(new Coordinate(1, 2), new Coordinate(2, 2));
            var pair2 = new CoordinatePair(new Coordinate(2, 2), new Coordinate(1, 2));

            Assert.IsTrue(pair1 == pair2);
            Assert.IsFalse(pair1 != pair2);
            Assert.AreEqual(pair1.GetHashCode(), pair2.GetHashCode());
            Assert.AreEqual(pair1, pair2);
            Assert.IsTrue(pair1.Equals(pair2));
        }
    }
}
