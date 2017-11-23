using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geometry.Tests
{
    [TestClass]
    public class StraightLineCurveTest
    {
        [TestMethod]
        public void GetPointFromStraightLineAtPosition()
        {
            var straightLine = new StraightLineCurve(new Vector2(0,1), new Vector2(2, 4));
            var point = straightLine.GetPoint(0.5);
            Assert.AreEqual(1, point.X);
            Assert.AreEqual(3, point.Y);
        }
    }
}
