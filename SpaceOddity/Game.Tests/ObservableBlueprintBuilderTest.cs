using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Tests.Mocks;
using Moq;

namespace Game.Tests
{
    [TestClass]
    public class ObservableBlueprintBuilderTest
    {
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprintBuilderObserver> mockObserver;
        private ObservableBlueprintBuilder observableBlueprintBuilder;

        [TestInitialize]
        public void Init()
        {
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>(MockBehavior.Loose);
            mockObserver = new Mock<IBlueprintBuilderObserver>(MockBehavior.Loose);
            observableBlueprintBuilder = new ObservableBlueprintBuilder(mockBlueprintBuilder.Object);
            observableBlueprintBuilder.AttachObserver(mockObserver.Object);
        }

        [TestMethod]
        public void CheckIfObservarbleBlueprintBuilderReturnsBaseDimensions()
        {
            mockBlueprintBuilder.SetupGet(x => x.Height).Returns(10);
            mockBlueprintBuilder.SetupGet(x => x.Width).Returns(12);
            Assert.AreEqual(10, observableBlueprintBuilder.Height);
            Assert.AreEqual(12, observableBlueprintBuilder.Width);
        }

        [TestMethod]
        public void CheckIfObservableBlueprintBuilderReturnsCorrectBlock()
        {
            MockBlock mockBlock = new MockBlock();
            mockBlueprintBuilder.Setup(x => x.GetBlock(3, 2)).Returns(mockBlock);
            Assert.AreEqual(mockBlock, observableBlueprintBuilder.GetBlock(3, 2));
            mockBlueprintBuilder.Verify(x => x.GetBlock(3, 2));
            
        }

        [TestMethod]
        public void CheckIfObserverIsInformedOfCreatedBlock()
        {
            mockBlueprintBuilder.Setup(x => x.CreateBlock(5, 6)).Returns(true);
            mockObserver.Setup(mock => mock.BlockCreated(mockBlueprintBuilder.Object, 5, 6));
            observableBlueprintBuilder.CreateBlock(5, 6);
            mockObserver.Verify(x => x.BlockCreated(observableBlueprintBuilder, 5, 6), Times.Once());
        }

        [TestMethod]
        public void ObserverShouldNotBeInformedIfNoBlockIsCreated()
        {
            mockBlueprintBuilder.Setup(x => x.CreateBlock(5, 6)).Returns(false);
            mockObserver.Setup(mock => mock.BlockCreated(mockBlueprintBuilder.Object, 5, 6));
            observableBlueprintBuilder.CreateBlock(5, 6);
            mockObserver.Verify(x => x.BlockCreated(observableBlueprintBuilder, 5, 6), Times.Never());
        }

        [TestMethod]
        public void CheckIfObserverIsInformedOfDeletedBlock()
        {
            mockBlueprintBuilder.Setup(x => x.DeleteBlock(5, 6)).Returns(true);
            mockObserver.Setup(mock => mock.BlockDeleted(mockBlueprintBuilder.Object, 5, 6));
            observableBlueprintBuilder.DeleteBlock(5, 6);
            mockObserver.Verify(x => x.BlockDeleted(observableBlueprintBuilder, 5, 6), Times.Once());
        }

        [TestMethod]
        public void ObserverShouldNotBeInformedIfNoBlockIsDeleted()
        {
            mockBlueprintBuilder.Setup(x => x.DeleteBlock(5, 6)).Returns(false);
            mockObserver.Setup(mock => mock.BlockDeleted(mockBlueprintBuilder.Object, 5, 6));
            observableBlueprintBuilder.DeleteBlock(5, 6);
            mockObserver.Verify(x => x.BlockDeleted(observableBlueprintBuilder, 5, 6), Times.Never());
        }
    }
}
