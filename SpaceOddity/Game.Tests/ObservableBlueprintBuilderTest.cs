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
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockObserver = new Mock<IBlueprintBuilderObserver>();
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
            //FLORIN, CE NAIBA, E BINE???

            MockBlock mockBlock = new MockBlock();
            mockBlueprintBuilder.Setup(x => x.GetBlock(3, 2)).Returns(mockBlock);
            Assert.AreEqual(mockBlock, observableBlueprintBuilder.GetBlock(3, 2));
            mockBlueprintBuilder.Verify(x => x.GetBlock(3, 2));
            
        }

        [TestMethod]
        public void CheckIfObserverIsInformedOfCreatedBlock()
        {
            mockBlueprintBuilder.Setup(x => x.CreateBlock(5, 6)).Returns(true);
            observableBlueprintBuilder.CreateBlock(5, 6);
            mockObserver.Verify(x => x.BlockCreated(mockBlueprintBuilder.Object, 5, 6)); //??? habar nu am
        }

        //[TestMethod]
        //public void ObserverShouldNotBeInformedIfNoBlockIsCreated()
        //{
        //    mockBlueprintBuilder.CanCreateBlock = false;
        //    observableBlueprintBuilder.CreateBlock(5, 6);
        //    Assert.AreEqual(0, mockObserver.X);
        //    Assert.AreEqual(0, mockObserver.Y);
        //}

        //[TestMethod]
        //public void CheckIfObserverIsInformedOfDeletedBlock()
        //{
        //    mockBlueprintBuilder.CanDeleteBlock = true;
        //    observableBlueprintBuilder.DeleteBlock(5, 6);
        //    Assert.AreEqual(6, mockObserver.X);
        //    Assert.AreEqual(5, mockObserver.Y);
        //}

        //[TestMethod]
        //public void ObserverShouldNotBeInformedIfNoBlockIsDeleted()
        //{
        //    mockBlueprintBuilder.CanDeleteBlock = false;
        //    observableBlueprintBuilder.DeleteBlock(5, 6);
        //    Assert.AreEqual(0, mockObserver.X);
        //    Assert.AreEqual(0, mockObserver.Y);
        //}
    }
}
