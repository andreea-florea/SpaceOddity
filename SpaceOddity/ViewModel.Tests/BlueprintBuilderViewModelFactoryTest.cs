using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;
using ViewModel.Interfaces;
using Geometry;
using System.Collections.Generic;
using ViewInterface;
using NaturalNumbersMath;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderViewModelFactoryTest
    {
        private Mock<IViewModelTilesFactory> mockTileFactory;
        private Mock<IWorldObjectFactory> mockBlockFactory;
        private IWorldObject[,] tiles;
        private Mock<IObservableBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprintBuilderController> mockController;
        private BlueprintBuilderViewModelFactory viewModelFactory;

        [TestInitialize]
        public void Initialize()
        {
            mockTileFactory = new Mock<IViewModelTilesFactory>();
            mockBlockFactory = new Mock<IWorldObjectFactory>();
            tiles = new IWorldObject[5, 3];
            mockBlueprintBuilder = new Mock<IObservableBlueprintBuilder>();
            mockTileFactory.Setup(factory => factory.CreateTiles(mockBlueprintBuilder.Object, It.IsAny<IRectangleSection>())).Returns(tiles);
            mockController = new Mock<IBlueprintBuilderController>();

            viewModelFactory = new BlueprintBuilderViewModelFactory(mockTileFactory.Object, mockBlockFactory.Object, mockController.Object);
        }

        [TestMethod]
        public void BlueprintBuilderViewModelIsCreatedWithCorrectEmptySlots()
        {
            var mockTile = new Mock<IWorldObject>();
            tiles[3, 2] = mockTile.Object;

            var mockRectangle = new Mock<IRectangleSection>();
            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, mockRectangle.Object);

            Assert.AreEqual(3, blueprintBuilderViewModel.Width);
            Assert.AreEqual(5, blueprintBuilderViewModel.Height);
            Assert.AreEqual(tiles[3, 2], blueprintBuilderViewModel.GetTile(new Coordinate(2, 3)));
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
