using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.Tests
{
    [TestClass]
    public class DoubleExtensionsTest
    {
        [TestMethod]
        public void CheckIfTwoDoubleNumbersShouldBeEqual()
        {
            var a = 2.0;
            var b = Math.Sqrt(2.0) * Math.Sqrt(2.0);
            Assert.IsTrue(a.CloseTo(b));
        }

        [TestMethod]
        public void CheckIfSmallerOrEqualToWorksCorrectly()
        {
            var a = 2.0;
            var b = 1.0;
            var c = Math.Sqrt(2.0) * Math.Sqrt(2.0);
            var d = 3.0;

            Assert.IsTrue(b.SmallerOrEqualTo(a));
            Assert.IsFalse(d.SmallerOrEqualTo(a));
            Assert.IsTrue(c.SmallerOrEqualTo(a));
        }
    }
}
