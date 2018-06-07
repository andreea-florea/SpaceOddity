using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.DataStructures;
using NaturalNumbersMath;
using Game.Enums;

namespace BlueprintBuildingViewModel.Tests.DataStructures
{
    [TestClass]
    public class DoubleEdgedPipePositionTest
    {
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
