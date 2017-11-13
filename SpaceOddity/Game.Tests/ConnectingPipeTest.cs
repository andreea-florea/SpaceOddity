using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Tests
{
    [TestClass]
    public class ConnectingPipeTest
    {
        private ConnectingPipe connectingPipe;

        [TestInitialize]
        public void Init()
        {
            connectingPipe = new ConnectingPipe(EdgeType.UP);
        }

        [TestMethod]
        public void CheckThatIsEqualToForConnectingPipesFunctionsCorrectly()
        {
            var pipe = new ConnectingPipe(EdgeType.UP);

            Assert.IsTrue(connectingPipe.IsEqualTo(pipe));
        }
    }
}
