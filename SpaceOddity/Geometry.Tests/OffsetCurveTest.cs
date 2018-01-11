using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Geometry.Tests
{
    [TestClass]
    public class OffsetCurveTest
    {
        [TestMethod]
        public void OffsetCurveReturnsCorrectPoint()
        {
            var mockCurve = new Mock<ICurve>();
            mockCurve.Setup(curve => curve.GetPoint(0.2)).Returns(new Vector2(1, 2));
            var offsetCurve = new OffsetCurve(mockCurve.Object, new Vector2(3, 4));
            Assert.AreEqual(new Vector2(4, 6), offsetCurve.GetPoint(0.2));
        }
    }
}
