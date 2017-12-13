using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfView.Tests
{
    [TestClass]
    public class ColorVectorTest
    {
        [TestMethod]
        public void ColorTestsAreSetCorrectly()
        {
            var color = new ColorVector(1.0, 0.5, 0.1);
            Assert.AreEqual(1.0, color.Red);
            Assert.AreEqual(0.5, color.Green);
            Assert.AreEqual(0.1, color.Blue);
        }

        [TestMethod]
        public void ColorIsGeneratedCorrectly()
        {
            var colorVector = new ColorVector(1.0, 0.5, 0.1);
            var color = colorVector.GetColor();

            Assert.AreEqual(255, color.R);
            Assert.AreEqual(127, color.G);
            Assert.AreEqual(25, color.B);
        }
    }
}
