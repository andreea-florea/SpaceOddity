using Game.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NaturalNumbersMath;

namespace Game.Tests
{
    [TestClass]
    public class ObservableBlueprintBuilderTest
    {
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IShipComponent> mockShipComponent;
        private Mock<IBlueprintBuilderObserver> mockObserver;
        private ObservableBlueprintBuilder observableBlueprintBuilder;

        [TestInitialize]
        public void Init()
        {
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>(MockBehavior.Loose);
            mockObserver = new Mock<IBlueprintBuilderObserver>(MockBehavior.Loose);
            mockShipComponent = new Mock<IShipComponent>();
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
            var position = new Coordinate(2, 3);
            var mockBlock = new Mock<IBlock>();
            mockBlueprintBuilder.Setup(x => x.GetBlock(position)).Returns(mockBlock.Object);
            Assert.AreEqual(mockBlock.Object, observableBlueprintBuilder.GetBlock(position));
            mockBlueprintBuilder.Verify(x => x.GetBlock(position), Times.Once());       
        }

        #region Block Creation
        [TestMethod]
        public void CheckIfObserverIsInformedOfCreatedBlock()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.CreateBlock(position)).Returns(true);
            observableBlueprintBuilder.CreateBlock(position);
            mockObserver.Verify(x => x.BlockCreated(observableBlueprintBuilder, position), Times.Once());
        }

        [TestMethod]
        public void ObserverShouldNotBeInformedIfNoBlockIsCreated()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.CreateBlock(position)).Returns(false);
            observableBlueprintBuilder.CreateBlock(position);
            mockObserver.Verify(x => x.BlockCreated(It.IsAny<IBlueprintBuilder>(), It.IsAny<Coordinate>()), Times.Never());
        }

        [TestMethod]
        public void CheckThatObserverSendsErrorMessageWhenCannotCreateBlock()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.CreateBlock(position)).Returns(false);
            observableBlueprintBuilder.CreateBlock(position);
            mockObserver.Verify(x => x.ErrorBlockNotCreated(observableBlueprintBuilder, position), Times.Once());
        }

        [TestMethod]
        public void CheckThatObserverDoesntSendErrorMessageWhenBlockCreated()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.CreateBlock(position)).Returns(true);
            observableBlueprintBuilder.CreateBlock(position);
            mockObserver.Verify(x => x.ErrorBlockNotCreated(It.IsAny<IBlueprintBuilder>(), It.IsAny<Coordinate>()), Times.Never());
        }
        #endregion

        #region Block Deletion
        [TestMethod]
        public void CheckIfObserverIsInformedOfDeletedBlock()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.DeleteBlock(position)).Returns(true);
            observableBlueprintBuilder.DeleteBlock(position);
            mockObserver.Verify(x => x.BlockDeleted(observableBlueprintBuilder, position), Times.Once());
        }

        [TestMethod]
        public void ObserverShouldNotBeInformedIfNoBlockIsDeleted()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.DeleteBlock(position)).Returns(false);
            observableBlueprintBuilder.DeleteBlock(position);
            mockObserver.Verify(x => x.BlockDeleted(It.IsAny<IBlueprintBuilder>(), It.IsAny<Coordinate>()), Times.Never());
        }      

        [TestMethod]
        public void CheckThatObserverSendsErrorMessageWhenCannotDeleteBlock()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.DeleteBlock(position)).Returns(false);
            observableBlueprintBuilder.DeleteBlock(position);
            mockObserver.Verify(x => x.ErrorBlockNotDeleted(observableBlueprintBuilder, position), Times.Once());
        }

        [TestMethod]
        public void CheckThatObserverDoesntSendErrorMessageWhenBlockDeleted()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.DeleteBlock(position)).Returns(true);
            observableBlueprintBuilder.DeleteBlock(position);
            mockObserver.Verify(x => x.ErrorBlockNotDeleted(It.IsAny<IBlueprintBuilder>(), It.IsAny<Coordinate>()), Times.Never());
        }
        #endregion

        #region Add Ship Component
        [TestMethod]
        public void CheckIfObserverIsInformedOfAddedShipComponent()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.AddShipComponent(position, mockShipComponent.Object)).Returns(true);
            observableBlueprintBuilder.AddShipComponent(position, mockShipComponent.Object);
            mockObserver.Verify(x => x.ShipComponentAdded(observableBlueprintBuilder, position), Times.Once());
        }

        [TestMethod]
        public void ObserverShouldNotBeInformedIfNoShipComponentIsAdded()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.AddShipComponent(position, mockShipComponent.Object)).Returns(false);
            observableBlueprintBuilder.AddShipComponent(position, mockShipComponent.Object);
            mockObserver.Verify(x => x.ShipComponentAdded(It.IsAny<IBlueprintBuilder>(), It.IsAny<Coordinate>()), Times.Never());
        }

        [TestMethod]
        public void CheckThatObserverSendsErrorMessageWhenCannotAddShipComponent()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.AddShipComponent(position, mockShipComponent.Object)).Returns(false);
            observableBlueprintBuilder.AddShipComponent(position, mockShipComponent.Object);
            mockObserver.Verify(x => x.ErrorShipComponentNotAdded(It.IsAny<IBlueprintBuilder>(), It.IsAny<Coordinate>()), Times.Once());
        }

        [TestMethod]
        public void CheckThatObserverDoesntSendErrorMessageWhenAddingShipComponent()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.AddShipComponent(position, mockShipComponent.Object)).Returns(true);
            observableBlueprintBuilder.AddShipComponent(position, mockShipComponent.Object);
            mockObserver.Verify(x => x.ErrorShipComponentNotAdded(It.IsAny<IBlueprintBuilder>(), It.IsAny<Coordinate>()), Times.Never());
        }
        #endregion

        #region Delete Ship Component
        [TestMethod]
        public void CheckIfObserverIsInformedOfDeletedShipComponent()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.DeleteShipComponent(position)).Returns(true);
            observableBlueprintBuilder.DeleteShipComponent(position);
            mockObserver.Verify(x => x.ShipComponentDeleted(observableBlueprintBuilder, position), Times.Once());
        }

        [TestMethod]
        public void ObserverShouldNotBeInformedIfNoShipComponentIsDeleted()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.DeleteShipComponent(position)).Returns(false);
            observableBlueprintBuilder.DeleteShipComponent(position);
            mockObserver.Verify(x => x.ShipComponentDeleted(It.IsAny<IBlueprintBuilder>(), It.IsAny<Coordinate>()), Times.Never());
        }

        [TestMethod]
        public void CheckThatObserverSendsErrorMessageWhenCannotDeleteShipComponent()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.DeleteShipComponent(position)).Returns(false);
            observableBlueprintBuilder.DeleteShipComponent(position);
            mockObserver.Verify(x => x.ErrorShipComponentNotDeleted(It.IsAny<IBlueprintBuilder>(), It.IsAny<Coordinate>()), Times.Once());
        }

        [TestMethod]
        public void CheckThatObserverDoesntSendErrorMessageWhenDeletingShipComponent()
        {
            var position = new Coordinate(6, 5);
            mockBlueprintBuilder.Setup(x => x.DeleteShipComponent(position)).Returns(true);
            observableBlueprintBuilder.DeleteShipComponent(position);
            mockObserver.Verify(x => x.ErrorShipComponentNotDeleted(It.IsAny<IBlueprintBuilder>(), It.IsAny<Coordinate>()), Times.Never());
        }
        #endregion
    }
}
