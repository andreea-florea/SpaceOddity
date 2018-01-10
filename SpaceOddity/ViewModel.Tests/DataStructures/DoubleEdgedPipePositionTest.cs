using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.DataStructures;
using NaturalNumbersMath;
using Game.Enums;

namespace ViewModel.Tests.DataStructures
{
    [TestClass]
    public class DoubleEdgedPipePositionTest
    {
        [TestMethod]
        public void DoubleEdgePipePositionIsCreatedCorrectly()
        {
            var position = new Coordinate(1, 2);
            var doubleEdgePipePosition = new PipePosition(position, EdgeType.RIGHT, EdgeType.UP);

            Assert.AreEqual(position, doubleEdgePipePosition.Position);
            Assert.AreEqual(EdgeType.RIGHT, doubleEdgePipePosition.FirstEdge);
            Assert.AreEqual(EdgeType.UP, doubleEdgePipePosition.SecondEdge);
        }

        [TestMethod]
        public void CheckDoubleEdgedPipePositionEqualities()
        {
            var pipe = new PipePosition(new Coordinate(1, 2), EdgeType.LEFT, EdgeType.RIGHT);
            var other = new PipePosition(new Coordinate(3, 2), EdgeType.LEFT, EdgeType.RIGHT);

            Assert.IsFalse(pipe == other);
            Assert.IsTrue(pipe != other);
        }

        [TestMethod]
        public void DoubleEdgedPipeAreEqualToReverseCounterpart()
        {
            var pipe = new PipePosition(new Coordinate(1, 2), EdgeType.LEFT, EdgeType.RIGHT);
            var other = new PipePosition(new Coordinate(1, 2), EdgeType.RIGHT, EdgeType.LEFT);

            Assert.IsTrue(pipe == other);
            Assert.IsFalse(pipe != other);
        }
    }
}
