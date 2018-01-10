using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Tests
{
    [TestClass]
    public class InterchangeablePairTest
    {
        [TestMethod]
        public void CheckIfInterchangeablePairIsSetupCorrectly()
        {
            var pair = new InterchangeablePair<int>(2, 3);

            Assert.AreEqual(2, pair.First);
            Assert.AreEqual(3, pair.Second);
        }

        [TestMethod]
        public void PositionInPairDoesNotMatter()
        {
            var pair1 = new InterchangeablePair<int>(2, 3);
            var samePair = new InterchangeablePair<int>(2, 3);
            var pair2 = new InterchangeablePair<int>(3, 2);
            var differentPair = new InterchangeablePair<int>(4, 3);

            Assert.IsTrue(pair1 == samePair);
            Assert.IsTrue(pair1 == pair2);
            Assert.IsTrue(pair1 != differentPair);
            Assert.IsFalse(pair1 != samePair);
            Assert.AreEqual(pair1, pair2);
            Assert.AreEqual(pair1.GetHashCode(), pair2.GetHashCode());
        }
    }
}
