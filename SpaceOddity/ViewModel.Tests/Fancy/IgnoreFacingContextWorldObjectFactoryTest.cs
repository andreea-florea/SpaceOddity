using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Fancy;
using ViewInterface;
using Moq;
using NaturalNumbersMath;
using ViewModel.Fancy.Iternal;

namespace ViewModel.Tests.Fancy
{
    [TestClass]
    public class IgnoreFacingContextWorldObjectFactoryTest
    {
        [TestMethod]
        public void FactoryAlwaysRedirectsToBaseFactory()
        {
            var mockObject = new Mock<IWorldObject>();
            var mockBaseFactory = new Mock<IWorldObjectFactory>();
            mockBaseFactory.Setup(factory => factory.CreateObject()).Returns(mockObject.Object);
            var objectFactory = new IgnoreFacingContextWorldObjectFactory(mockBaseFactory.Object);

            var createdObject = objectFactory.CreateObject(new FacingPosition());

            mockBaseFactory.Verify(factory => factory.CreateObject(), Times.Once());
            Assert.AreEqual(mockObject.Object, createdObject);
        }
    }
}
