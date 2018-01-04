using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game;
using NaturalNumbersMath;
using ViewModel.Extensions;

namespace ViewModel.Tests.Extensions
{
    [TestClass]
    public class CoordinateEdgeTypeExtensions
    {
        [TestMethod]
        public void ConvertCoordinateToEdgeTypeCorrectly()
        {
            Assert.AreEqual(EdgeType.UP, Coordinates.Up.ToEdgeType());
            Assert.AreEqual(EdgeType.DOWN, Coordinates.Down.ToEdgeType());
            Assert.AreEqual(EdgeType.LEFT, Coordinates.Left.ToEdgeType());
            Assert.AreEqual(EdgeType.RIGHT, Coordinates.Right.ToEdgeType());
        }

        [TestMethod]
        public void ConvertEdgeTypeToCoordinateCorrectly()
        {
            Assert.AreEqual(Coordinates.Up, EdgeType.UP.ToCoordinate());
            Assert.AreEqual(Coordinates.Down, EdgeType.DOWN.ToCoordinate());
            Assert.AreEqual(Coordinates.Left, EdgeType.LEFT.ToCoordinate());
            Assert.AreEqual(Coordinates.Right, EdgeType.RIGHT.ToCoordinate());
        }

        [TestMethod]
        public void CountEdgeTypeConvertsToPointZero()
        {
            Assert.AreEqual(EdgeType.COUNT, Coordinates.Zero.ToEdgeType());
            Assert.AreEqual(Coordinates.Zero, EdgeType.COUNT.ToCoordinate());
        }
    }
}
