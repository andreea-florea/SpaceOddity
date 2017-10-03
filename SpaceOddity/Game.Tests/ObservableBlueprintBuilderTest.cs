using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Tests.Mocks;

namespace Game.Tests
{
    [TestClass]
    public class ObservableBlueprintBuilderTest
    {
        private MockBlueprintBuilder mockBlueprintBuilder;
        private MockBlueprintBuilderObserver mockObserver;
        private ObservableBlueprintBuilder observableBlueprintBuilder;

        [TestInitialize]
        public void Init()
        {
            mockBlueprintBuilder = new MockBlueprintBuilder(10, 12);
            mockObserver = new MockBlueprintBuilderObserver();
            observableBlueprintBuilder = new ObservableBlueprintBuilder(mockBlueprintBuilder);
            observableBlueprintBuilder.AttachObserver(mockObserver);
        }

        [TestMethod]
        public void CheckIfObservarbleBlueprintBuilderReturnsBaseDimensions()
        {
            Assert.AreEqual(10, observableBlueprintBuilder.Height);
            Assert.AreEqual(12, observableBlueprintBuilder.Width);
        }

        [TestMethod]
        public void CheckIfObservableBlueprintBuilderReturnsCorrectBlock()
        {
            Assert.AreEqual(mockBlueprintBuilder.Block, observableBlueprintBuilder.GetBlock(3, 2));
            Assert.AreEqual(2, mockBlueprintBuilder.GetBlockX);
            Assert.AreEqual(3, mockBlueprintBuilder.GetBlockY);
        }

        [TestMethod]
        public void CheckIfObserverIsInformedOfCreatedBlock()
        {
            mockBlueprintBuilder.CanCreateBlock = true;
            observableBlueprintBuilder.CreateBlock(5, 6);
            Assert.AreEqual(6, mockObserver.X);
            Assert.AreEqual(5, mockObserver.Y);
        }

        [TestMethod]
        public void ObserverShouldNotBeInformedIfNoBlockIsCreated()
        {
            mockBlueprintBuilder.CanCreateBlock = false;
            observableBlueprintBuilder.CreateBlock(5, 6);
            Assert.AreEqual(0, mockObserver.X);
            Assert.AreEqual(0, mockObserver.Y);
        }

        [TestMethod]
        public void CheckIfObserverIsInformedOfDeletedBlock()
        {
            mockBlueprintBuilder.CanDeleteBlock = true;
            observableBlueprintBuilder.DeleteBlock(5, 6);
            Assert.AreEqual(6, mockObserver.X);
            Assert.AreEqual(5, mockObserver.Y);
        }

        [TestMethod]
        public void ObserverShouldNotBeInformedIfNoBlockIsDeleted()
        {
            mockBlueprintBuilder.CanDeleteBlock = false;
            observableBlueprintBuilder.DeleteBlock(5, 6);
            Assert.AreEqual(0, mockObserver.X);
            Assert.AreEqual(0, mockObserver.Y);
        }
    }
}
