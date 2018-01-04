using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Enums;

namespace Game.Tests
{
    [TestClass]
    public class DoubleEdgedPipeTest
    {
        private DoubleEdgedPipe doubleEdgedPipe;

        [TestInitialize]
        public void Init()
        {
            doubleEdgedPipe = new DoubleEdgedPipe(EdgeType.UP, EdgeType.DOWN);
        }

        [TestMethod]
        public void CheckThatIsEqualToForDoubleEdgedPipeForConnectingPipesReturnsFalse()
        {
            var pipe = new DoubleEdgedPipe(EdgeType.UP, EdgeType.LEFT);

            Assert.IsFalse(doubleEdgedPipe.IsEqualTo(pipe));
        }

        [TestMethod]
        public void CheckThatIsEqualToForDoubleEdgedPipeForConnectingPipesReturnsTrue()
        {
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);

            Assert.IsTrue(doubleEdgedPipe.IsEqualTo(pipe));
        }

        [TestMethod]
        public void CheckThatHashCodesForSameDoubleEdgedPipeAreEqual()
        {
            var pipe1 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            var pipe2 = new DoubleEdgedPipe(EdgeType.UP, EdgeType.DOWN);

            Assert.AreEqual(pipe1.GetHashCode(), pipe2.GetHashCode());
        }

        [TestMethod]
        public void CheckThatHashCodesForDifferentDoubleEdgedPipeAreDifferent()
        {
            var pipe1 = new DoubleEdgedPipe(EdgeType.LEFT, EdgeType.UP);
            var pipe2 = new DoubleEdgedPipe(EdgeType.UP, EdgeType.DOWN);

            Assert.AreNotEqual(pipe1.GetHashCode(), pipe2.GetHashCode());
        }
    }
}
