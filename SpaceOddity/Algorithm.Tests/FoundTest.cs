using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Tests
{
    [TestClass]
    public class FoundTest
    {
        [TestMethod]
        public void FoundPropertiesAssignedCorrectly()
        {
            var found = new Found<int>(true, 4);
            Assert.IsTrue(found.IsFound);
            Assert.AreEqual(4, found.Element);
        }
    }
}
