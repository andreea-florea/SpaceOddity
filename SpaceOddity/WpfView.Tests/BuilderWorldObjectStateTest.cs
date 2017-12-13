using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfView.Tests
{
    [TestClass]
    public class BuilderWorldObjectStateTest
    {
        [TestMethod]
        public void BuilderWorldObjectStatePropertiesAreSetCorrectly()
        {
            var fill = new ColorVector(1.0, 0.5, 1.0);
            var border = new ColorVector(0.2, 0.3, 0.4);
            var state = new BuilderWorldObjectState(fill, border);
            Assert.AreEqual(fill, state.Fill);
            Assert.AreEqual(border, state.Border);
        }
    }
}
