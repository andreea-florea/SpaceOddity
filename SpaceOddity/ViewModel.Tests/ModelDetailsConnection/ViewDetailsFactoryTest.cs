using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.ModelDetailsConnection;
using Moq;
using Algorithms;

namespace ViewModel.Tests.ModelDetailsConnection
{
    [TestClass]
    public class ViewDetailsFactoryTest
    {
        [TestMethod]
        public void CreateElementsBasedOnDetailsCorrectly()
        {
            var mockDetails = new Mock<IDetails<double>>();
            var viewFactories = new IFactory<IWorldObject>[5];
            var mockFactory = new Mock<IFactory<IWorldObject>>();
            viewFactories[3] = mockFactory.Object;
            var mockObject = new Mock<IWorldObject>();
            var detailsFactory = new ViewDetailsFactory<IWorldObject, double>(mockDetails.Object, viewFactories);

            mockDetails.SetupGet(details => details[4.0]).Returns(3);
            mockFactory.Setup(factory => factory.Create()).Returns(mockObject.Object);
            Assert.AreEqual(mockObject.Object, detailsFactory.Create(4.0));
        }
    }
}
