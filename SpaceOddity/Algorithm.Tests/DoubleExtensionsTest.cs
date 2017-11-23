using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Tests
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
    }
}
