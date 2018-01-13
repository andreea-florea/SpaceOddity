using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.Fancy;
using ViewInterface;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Fancy.Iternal;
using Algorithm;
using ViewModel;

namespace BlueprintBuildingViewModel.Tests.Fancy
{
    [TestClass]
    public class IgnoreFacingContextWorldObjectFactoryTest
    {
        [TestMethod]
        public void FactoryAlwaysRedirectsToBaseFactory()
        {
            var mockObject = new Mock<IWorldObject>();
            var mockBaseFactory = new Mock<IFactory<IWorldObject>>();
            mockBaseFactory.Setup(factory => factory.Create()).Returns(mockObject.Object);
            var objectFactory = new IgnoreFacingContextWorldObjectFactory(mockBaseFactory.Object);

            var createdObject = objectFactory.Create(new FacingPosition());

            mockBaseFactory.Verify(factory => factory.Create(), Times.Once());
            Assert.AreEqual(mockObject.Object, createdObject);
        }
    }
}
