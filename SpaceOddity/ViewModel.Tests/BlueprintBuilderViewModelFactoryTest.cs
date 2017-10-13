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
        private Mock<IWorldObjectFactory> mockTileFactory;
        private Mock<IWorldObjectFactory> mockBlockFactory;
        private Mock<IWorldObject> mockObject;
        private Mock<IObservableBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprintBuilderController> mockController;
        private BlueprintBuilderViewModelFactory viewModelFactory;

        [TestInitialize]
        public void Initialize()
        {
            mockTileFactory = new Mock<IWorldObjectFactory>();
            mockBlockFactory = new Mock<IWorldObjectFactory>();
            mockObject = new Mock<IWorldObject>();
            mockBlueprintBuilder = new Mock<IObservableBlueprintBuilder>();
            mockController = new Mock<IBlueprintBuilderController>();

            viewModelFactory = new BlueprintBuilderViewModelFactory(mockTileFactory.Object, mockBlockFactory.Object, mockController.Object);

            mockObject.SetupAllProperties();
        }

        [TestMethod]
        public void BlueprintBuilderViewModelIsCreatedWithCorrectEmptySlots()
        {
            mockTileFactory.Setup(factory => factory.CreateObject()).Returns(mockObject.Object);

            mockBlueprintBuilder.Setup(builder => builder.Width).Returns(3);
            mockBlueprintBuilder.Setup(builder => builder.Height).Returns(5);

            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(), new Vector2()));

            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);

            Assert.AreEqual(3, blueprintBuilderViewModel.Width);
            Assert.AreEqual(5, blueprintBuilderViewModel.Height);
            Assert.AreEqual(mockObject.Object, blueprintBuilderViewModel.GetTile(2, 3));
        }

        [TestMethod]
        public void ViewModelObjectsAreCreatedAtCorrectPositions()
        {
            var testMockObject = new Mock<IWorldObject>();
            testMockObject.SetupAllProperties();

            mockTileFactory.SetupSequence(factory => factory.CreateObject())
                .Returns(mockObject.Object)
                .Returns(mockObject.Object)
                .Returns(mockObject.Object)
                .Returns(testMockObject.Object)
                .Returns(mockObject.Object)
                .Returns(mockObject.Object);

            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));

            mockBlueprintBuilder.Setup(builder => builder.Width).Returns(2);
            mockBlueprintBuilder.Setup(builder => builder.Height).Returns(3);
            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);

            Assert.AreEqual(4, testMockObject.Object.Position.X);
            Assert.AreEqual(5, testMockObject.Object.Position.Y);
        }

        [TestMethod]
        public void ViewModelObjectsAreCreatedWithCorrectScale()
        {
            mockTileFactory.Setup(factory => factory.CreateObject()).Returns(mockObject.Object);

            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));

            mockBlueprintBuilder.Setup(builder => builder.Width).Returns(2);
            mockBlueprintBuilder.Setup(builder => builder.Height).Returns(2);
            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);

            Assert.AreEqual(2, mockObject.Object.Scale.X);
            Assert.AreEqual(3, mockObject.Object.Scale.Y);
        }

        [TestMethod]
        public void CheckIfViewModelTilesAreAssignedControl()
        {
            var testMockObject = new Mock<IWorldObject>();
            testMockObject.SetupAllProperties();

            mockTileFactory.SetupSequence(factory => factory.CreateObject())
                .Returns(mockObject.Object)
                .Returns(mockObject.Object)
                .Returns(testMockObject.Object)
                .Returns(mockObject.Object);

            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));

            mockBlueprintBuilder.Setup(builder => builder.Width).Returns(2);
            mockBlueprintBuilder.Setup(builder => builder.Height).Returns(2);
            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);

            mockController.Verify(controller => controller.AssignTileControl(mockBlueprintBuilder.Object, mockObject.Object, 0, 0), Times.Once());
            mockController.Verify(controller => controller.AssignTileControl(mockBlueprintBuilder.Object, mockObject.Object, 1, 0), Times.Once());
            mockController.Verify(controller => controller.AssignTileControl(mockBlueprintBuilder.Object, testMockObject.Object, 0, 1), Times.Once());
            mockController.Verify(controller => controller.AssignTileControl(mockBlueprintBuilder.Object, mockObject.Object, 1, 1), Times.Once());
        }

        [TestMethod]
        public void CheckIfViewModelIsAttachedAsAnObserverToBlueprintBuilder()
        {
            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));
            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);
            mockBlueprintBuilder.Verify(builder => builder.AttachObserver(blueprintBuilderViewModel), Times.Once());
        }
    }
}
