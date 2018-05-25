using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.ModelDetailsConnection;

namespace ViewModel.Tests.ModelDetailsConnection
{
    [TestClass]
    public class DetailsTest
    {
        [TestMethod]
        public void TestDetails()
        {
            var details = new Details<double>();

            details.Add(3.0, 5);
            Assert.AreEqual(5, details[3.0]);
            details.Remove(5);
            try
            {
                var value = details[3.0];
                Assert.Fail();
            }
            catch(Exception) { }
        }
    }
}
