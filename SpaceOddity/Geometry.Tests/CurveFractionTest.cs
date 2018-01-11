using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Geometry.Tests
{
    [TestClass]
    public class CurveFractionTest
    {
        [TestMethod]
        public void OffsetCurveReturnsCorrectPoint()
        {
            var mockCurve = new Mock<ICurve>();
            mockCurve.Setup(curve => curve.GetPoint(0.30000000000000004)).Returns(new Vector2(1, 2));
            var curveFraction = new CurveFraction(mockCurve.Object, 0.1, 0.4);
            Assert.AreEqual(new Vector2(1, 2), curveFraction.GetPoint(0.5));
        }
    }
}
