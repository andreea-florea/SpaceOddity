using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Tests
{
    [TestClass]
    public class JetEngineTest
    {
        JetEngine jetEngine;

        [TestInitialize]
        public void Init()
        {
            jetEngine = new JetEngine();
        }

        [TestMethod]
        public void CheckIfCanCreateBlock()
        {

        }
    }
}
