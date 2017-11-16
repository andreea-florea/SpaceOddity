using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System.Collections.Generic;

namespace Algorithms.Tests
{
    [TestClass]
    public class ListExtensionsTest
    {
        [TestMethod]
        public void CheckIfAllPairsAreCalled()
        {
            var list = new int[] { 1, 2, 3 };
            var testedPairs = new List<int[]>();

            list.ForeachPair((x, y) => testedPairs.Add(new int[2] { x, y }));

            Assert.AreEqual(1, testedPairs[0][0]);
            Assert.AreEqual(2, testedPairs[0][1]);
            Assert.AreEqual(1, testedPairs[1][0]);
            Assert.AreEqual(3, testedPairs[1][1]);
            Assert.AreEqual(2, testedPairs[2][0]);
            Assert.AreEqual(3, testedPairs[2][1]);
        }
    }
}
