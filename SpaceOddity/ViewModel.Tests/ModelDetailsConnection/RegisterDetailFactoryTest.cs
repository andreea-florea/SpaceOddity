using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.ModelDetailsConnection;
using Algorithms;
using Moq;

namespace ViewModel.Tests.ModelDetailsConnection
{
    [TestClass]
    public class RegisterDetailFactoryTest
    {
        [TestMethod]
        public void FactoryRegistersDetailCorrectly()
        {
            var mockFactory = new Mock<IFactory<double>>();
            var mockDetails = new Mock<IDetails<double>>();
            var registerFactory = new RegisterDetailFactory<double>(mockFactory.Object, mockDetails.Object, 5);

            mockFactory.Setup(factory => factory.Create()).Returns(4.0);

            Assert.AreEqual(4.0, registerFactory.Create());
            mockDetails.Verify(details => details.Add(4.0, 5), Times.Once());
        }

        [TestMethod]
        public void FactoryRegistersDetailCorrectlyWithContext()
        {
            var mockFactory = new Mock<IFactory<double, float>>();
            var mockDetails = new Mock<IDetails<double>>();
            var registerFactory = new RegisterDetailFactory<double, float>(mockFactory.Object, mockDetails.Object, 5);

            mockFactory.Setup(factory => factory.Create(6.0f)).Returns(4.0);

            Assert.AreEqual(4.0, registerFactory.Create(6.0f));
            mockDetails.Verify(details => details.Add(4.0, 5), Times.Once());
        }
    }
}
