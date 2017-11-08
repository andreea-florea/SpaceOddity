﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;

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
        private Mock<IWorldObjectFactory> mockShipComponentsFactory;
        private IWorldObject[,] tiles;
        private Mock<IObservableBlueprintBuilder> mockBlueprintBuilder;
        private BlueprintBuilderViewModelFactory viewModelFactory;

        [TestInitialize]
        public void Initialize()
        {
            mockTileFactory = new Mock<IViewModelTilesFactory>();
            mockBlockFactory = new Mock<IWorldObjectFactory>();
            mockShipComponentsFactory = new Mock<IWorldObjectFactory>();
            tiles = new IWorldObject[5, 3];
            mockBlueprintBuilder = new Mock<IObservableBlueprintBuilder>();
            mockTileFactory.Setup(factory => 
                factory.CreateTiles(It.IsAny<IBlueprintBuilderControlAssigner>(), It.IsAny<Coordinate>(), It.IsAny<IRectangleSection>())).Returns(tiles);

            viewModelFactory = 
                new BlueprintBuilderViewModelFactory(mockTileFactory.Object,
                    mockBlockFactory.Object, mockShipComponentsFactory.Object);
        }

        [TestMethod]
        public void BlueprintBuilderViewModelIsCreatedWithCorrectEmptySlots()
        {
            var position = new Coordinate(2, 3);
            var mockTile = new Mock<IWorldObject>();
            tiles.Set(position, mockTile.Object);

            var mockRectangle = new Mock<IRectangleSection>();
            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, mockRectangle.Object);

            Assert.AreEqual(tiles.Get(position), blueprintBuilderViewModel.GetTile(position));
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

        [TestMethod]
        public void CheckIfViewModelIsCreatedWithController()
        {
            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));

            var mockBlock = new Mock<IWorldObject>();
            mockBlock.SetupAllProperties();
            mockBlockFactory.Setup(factory => factory.CreateObject()).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 6));
            var blueprintBuilderViewModel = 
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);

            var position = new Coordinate(1, 1);
            tiles.Set(position, new Mock<IWorldObject>().Object);
            blueprintBuilderViewModel.BlockCreated(mockBlueprintBuilder.Object, position);
            blueprintBuilderViewModel.GetBlock(position).RightClickAction.Execute();
            mockBlueprintBuilder.Verify(builder => builder.DeleteBlock(position), Times.Once());
        }

        [TestMethod]
        public void CheckIfFactoryIsUsedToCreateShipComponents()
        {
            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 6));
            mockShipComponentsFactory.Setup(factory => factory.CreateObject()).Returns(new Mock<IWorldObject>().Object);

            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);
            var mockTile = new Mock<IWorldObject>();
            tiles.Set(new Coordinate(2, 3), mockTile.Object);
            blueprintBuilderViewModel.ShipComponentAdded(mockBlueprintBuilder.Object, new Coordinate(2, 3));

            mockShipComponentsFactory.Verify(factory => factory.CreateObject(), Times.Once());
        }
    }
}
