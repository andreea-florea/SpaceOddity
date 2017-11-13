using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
