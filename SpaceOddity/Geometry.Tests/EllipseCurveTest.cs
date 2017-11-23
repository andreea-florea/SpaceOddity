using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geometry.Tests
{
    [TestClass]
    public class EllipseCurveTest
    {
        [TestMethod]
        public void GetCircleCurveCorrectPoint()
        {
            var ellipseCurve = new EllipseCurve(new Vector2(5, 10));
            var angle = 0.643501108793284;
            var point = ellipseCurve.GetPoint(angle);
            Assert.AreEqual(new Vector2(4, 6), ellipseCurve.GetPoint(angle));
        }
    }
}
