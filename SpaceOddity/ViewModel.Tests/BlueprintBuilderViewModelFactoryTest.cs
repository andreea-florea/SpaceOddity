using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;
using ViewModel.Interfaces;
using Geometry;
using System.Collections.Generic;
using ViewInterface;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderViewModelFactoryTest
    {
        private Mock<IWorldObjectFactory> mockSlotFactory;
        private Mock<IWorldObject> mockObject;
        private Mock<IObservableBlueprintBuilder> mockblueprintBuilder;
        private BlueprintBuilderViewModelFactory viewModelFactory;

        [TestInitialize]
        public void Init()
        {
            mockSlotFactory = new Mock<IWorldObjectFactory>();
            mockObject = new Mock<IWorldObject>();
            mockblueprintBuilder = new Mock<IObservableBlueprintBuilder>();

            viewModelFactory = new BlueprintBuilderViewModelFactory(mockSlotFactory.Object);

            mockObject.SetupAllProperties();
        }

        [TestMethod]
        public void BlueprintBuilderViewModelIsCreatedWithCorrectEmptySlots()
        {
            mockSlotFactory.Setup(factory => factory.CreateObject()).Returns(mockObject.Object);

            mockblueprintBuilder.Setup(builder => builder.Width).Returns(3);
            mockblueprintBuilder.Setup(builder => builder.Height).Returns(5);

            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(), new Vector2()));

            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockblueprintBuilder.Object, rectangle.Object);

            Assert.AreEqual(3, blueprintBuilderViewModel.Width);
            Assert.AreEqual(5, blueprintBuilderViewModel.Height);
            Assert.AreEqual(mockObject.Object, blueprintBuilderViewModel.GetSlot(3, 2));
        }

        [TestMethod]
        public void ViewModelObjectsAreCreatedAtCorrectPositions()
        {
            var testMockObject = new Mock<IWorldObject>();
            testMockObject.SetupAllProperties();

            mockSlotFactory.SetupSequence(factory => factory.CreateObject())
                .Returns(mockObject.Object)
                .Returns(mockObject.Object)
                .Returns(mockObject.Object)
                .Returns(testMockObject.Object)
                .Returns(mockObject.Object)
                .Returns(mockObject.Object);

            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));

            mockblueprintBuilder.Setup(builder => builder.Width).Returns(2);
            mockblueprintBuilder.Setup(builder => builder.Height).Returns(3);
            var blueprintBuilderViewModel = 
                viewModelFactory.CreateViewModel(mockblueprintBuilder.Object, rectangle.Object);

            Assert.AreEqual(4, testMockObject.Object.Position.X);
            Assert.AreEqual(5, testMockObject.Object.Position.Y);
        }

        [TestMethod]
        public void ViewModelObjectsAreCreatedWithCorrectScale()
        {
            mockSlotFactory.Setup(factory => factory.CreateObject()).Returns(mockObject.Object);

            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));

            mockblueprintBuilder.Setup(builder => builder.Width).Returns(2);
            mockblueprintBuilder.Setup(builder => builder.Height).Returns(2);
            var blueprintBuilderViewModel = 
                viewModelFactory.CreateViewModel(mockblueprintBuilder.Object, rectangle.Object);

            Assert.AreEqual(2, mockObject.Object.Scale.X);
            Assert.AreEqual(3, mockObject.Object.Scale.Y);
        }
    }
}
