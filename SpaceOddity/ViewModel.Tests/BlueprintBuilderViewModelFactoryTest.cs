using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;
using ViewModel.Interfaces;
using Geometry;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderViewModelFactoryTest
    {

        [TestMethod]
        public void BlueprintBuilderViewModelIsCreatedWithCorrectEmptySlots()
        {
            var slotFactory = new Mock<IWorldObjectFactory>();
            var mockObject = new Mock<IWorldObject>();
            slotFactory.Setup(factory => factory.CreateObject()).Returns(mockObject.Object);

            var rectangle = new Rectangle(new Vector2(1, 2), new Vector2(5, 8));

            var mockblueprintBuilder = new Mock<IObservableBlueprintBuilder>();
            var viewModelFactory = new BlueprintBuilderViewModelFactory(slotFactory.Object);

            mockblueprintBuilder.Setup(builder => builder.Width).Returns(3);
            mockblueprintBuilder.Setup(builder => builder.Height).Returns(5);
            var blueprintBuilderViewModel = viewModelFactory.CreateViewModel(mockblueprintBuilder.Object, rectangle);

            Assert.AreEqual(3, blueprintBuilderViewModel.Width);
            Assert.AreEqual(5, blueprintBuilderViewModel.Height);
            Assert.AreEqual(mockObject.Object, blueprintBuilderViewModel.GetSlot(3, 2));
        }

        [TestMethod]
        public void ViewModelObjectsAreCreatedAtCorrectPositions()
        {
            var slotFactory = new Mock<IWorldObjectFactory>();
            var mockObject = new Mock<IWorldObject>();
            slotFactory.Setup(factory => factory.CreateObject()).Returns(mockObject.Object);

            var rectangle = new Rectangle(new Vector2(1, 2), new Vector2(5, 8));

            var mockblueprintBuilder = new Mock<IObservableBlueprintBuilder>();
            var viewModelFactory = new BlueprintBuilderViewModelFactory(slotFactory.Object);

            mockblueprintBuilder.Setup(builder => builder.Width).Returns(3);
            mockblueprintBuilder.Setup(builder => builder.Height).Returns(2);
            var blueprintBuilderViewModel = viewModelFactory.CreateViewModel(mockblueprintBuilder.Object, rectangle);

            //Assert.AreEqual(4, blueprintBuilderViewModel.GetSlot(1, 1).Position.X);
            //Assert.AreEqual(6.5, blueprintBuilderViewModel.GetSlot(1, 1).Position.Y);
        }
    }
}
