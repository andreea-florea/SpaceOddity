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
            Assert.AreEqual(EdgeType.UP, Coordinates.Up.GetEdgeType());
            Assert.AreEqual(EdgeType.DOWN, Coordinates.Down.GetEdgeType());
            Assert.AreEqual(EdgeType.LEFT, Coordinates.Left.GetEdgeType());
            Assert.AreEqual(EdgeType.RIGHT, Coordinates.Right.GetEdgeType());
        }
    }
}
