using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geometry.Tests
{
    [TestClass]
    public class Vector2sTest
    {
        [TestMethod]
        public void VectorDirectionsAreCorrect()
        {
            Assert.AreEqual(0, Vector2s.Zero.X);
            Assert.AreEqual(0, Vector2s.Zero.Y);
            Assert.AreEqual(0, Vector2s.Up.X);
            Assert.AreEqual(1, Vector2s.Up.Y);
            Assert.AreEqual(1, Vector2s.Right.X);
            Assert.AreEqual(0, Vector2s.Right.Y);
            Assert.AreEqual(-1, Vector2s.Left.X);
            Assert.AreEqual(0, Vector2s.Left.Y);
            Assert.AreEqual(0, Vector2s.Down.X);
            Assert.AreEqual(-1, Vector2s.Down.Y);
        }
    }
}
