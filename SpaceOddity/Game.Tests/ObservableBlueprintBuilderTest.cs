using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var mockBlock = new Mock<IBlock>();
            mockBlueprintBuilder.Setup(x => x.GetBlock(3, 2)).Returns(mockBlock.Object);
            Assert.AreEqual(mockBlock.Object, observableBlueprintBuilder.GetBlock(3, 2));
            mockBlueprintBuilder.Verify(x => x.GetBlock(3, 2), Times.Once());       
        }

        [TestMethod]
        public void CheckIfObserverIsInformedOfCreatedBlock()
        {
            mockBlueprintBuilder.Setup(x => x.CreateBlock(5, 6)).Returns(true);
            observableBlueprintBuilder.CreateBlock(5, 6);
            mockObserver.Verify(x => x.BlockCreated(observableBlueprintBuilder, 5, 6), Times.Once());
        }

        [TestMethod]
        public void ObserverShouldNotBeInformedIfNoBlockIsCreated()
        {
            mockBlueprintBuilder.Setup(x => x.CreateBlock(5, 6)).Returns(false);
            observableBlueprintBuilder.CreateBlock(5, 6);
            mockObserver.Verify(x => x.BlockCreated(It.IsAny<IBlueprintBuilder>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }

        [TestMethod]
        public void CheckIfObserverIsInformedOfDeletedBlock()
        {
            mockBlueprintBuilder.Setup(x => x.DeleteBlock(5, 6)).Returns(true);
            observableBlueprintBuilder.DeleteBlock(5, 6);
            mockObserver.Verify(x => x.BlockDeleted(observableBlueprintBuilder, 5, 6), Times.Once());
        }

        [TestMethod]
        public void ObserverShouldNotBeInformedIfNoBlockIsDeleted()
        {
            mockBlueprintBuilder.Setup(x => x.DeleteBlock(5, 6)).Returns(false);
            observableBlueprintBuilder.DeleteBlock(5, 6);
            mockObserver.Verify(x => x.BlockDeleted(It.IsAny<IBlueprintBuilder>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }
    }
}
