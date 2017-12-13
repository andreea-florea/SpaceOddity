using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;

using Geometry;
using System.Collections.Generic;
using ViewInterface;
using NaturalNumbersMath;
using ViewModel.Actions;
using ViewModel.Controller;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderViewModelFactoryTest
    {
        private Mock<IViewModelTilesFactory> mockTileFactory;
        private Mock<IRenderableFactory> mockBlockFactory;
        private Mock<IRenderableFactory> mockShipComponentsFactory;
        private Mock<IRenderableFactory> mockPipeLinkFactory;
        private IBuilderWorldObject[,] tiles;
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprint> mockBlueprint;
        private BlueprintBuilderViewModelFactory viewModelFactory;

        [TestInitialize]
        public void Initialize()
        {
            mockTileFactory = new Mock<IViewModelTilesFactory>();
            mockBlockFactory = new Mock<IRenderableFactory>();
            mockShipComponentsFactory = new Mock<IRenderableFactory>();
            mockPipeLinkFactory = new Mock<IRenderableFactory>();

            var mockEmptyTile = new Mock<IBuilderWorldObject>();
            tiles = new IBuilderWorldObject[6, 4];
            var rect = new CoordinateRectangle(Coordinates.Zero, new Coordinate(4, 6));
            foreach (var coordinate in rect.Points)
            {
                tiles.Set(coordinate, mockEmptyTile.Object);
            }

            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockBlueprint = new Mock<IBlueprint>();
            mockTileFactory.Setup(factory => 
                factory.CreateTiles(It.IsAny<Coordinate>(), It.IsAny<IRectangleSection>())).Returns(tiles);

            viewModelFactory = new BlueprintBuilderViewModelFactory(
                mockTileFactory.Object,
                mockBlockFactory.Object, 
                mockShipComponentsFactory.Object,
                mockPipeLinkFactory.Object);
        }

        [TestMethod]
        public void BlueprintBuilderViewModelIsCreatedWithCorrectEmptySlots()
        {
            var position = new Coordinate(2, 3);
            var mockTile = new Mock<IBuilderWorldObject>();
            tiles.Set(position, mockTile.Object);

            mockBlueprintBuilder.SetupGet(builder => builder.Dimensions).Returns(new Coordinate(3, 2));
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
            mockBlueprintBuilder.SetupGet(builder => builder.Dimensions).Returns(new Coordinate(3, 2));
            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);
            mockBlueprintBuilder.Verify(builder => builder.AttachObserver(blueprintBuilderViewModel), Times.Once());
        }

        [TestMethod]
        public void CheckIfViewModelIsCreatedWithController()
        {
            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));

            var mockRenderable = new Mock<IRenderable>();
            mockRenderable.SetupSet(block => block.RightClickAction = It.IsAny<BlockSelectAction>()).Verifiable();
            mockBlockFactory.Setup(factory => factory.CreateRenderable()).Returns(mockRenderable.Object);
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 6));
            var blueprintBuilderViewModel = 
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);

            var position = new Coordinate(1, 1);
            tiles.Set(position, new Mock<IBuilderWorldObject>().Object);
            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);
            blueprintBuilderViewModel.GetBlock(position);
            mockRenderable.VerifySet(block => block.RightClickAction = It.IsAny<BlockCancelAction>());
        }

        [TestMethod]
        public void CheckIfFactoryIsUsedToCreateShipComponents()
        {
            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 6));
            mockShipComponentsFactory.Setup(factory => factory.CreateRenderable()).Returns(new Mock<IRenderable>().Object);

            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);
            var mockTile = new Mock<IBuilderWorldObject>();
            tiles.Set(new Coordinate(2, 3), mockTile.Object);
            blueprintBuilderViewModel.ShipComponentAdded(mockBlueprint.Object, new Coordinate(2, 3));

            mockShipComponentsFactory.Verify(factory => factory.CreateRenderable(), Times.Once());
        }

        [TestMethod]
        public void CheckIfFactoryIsUsedToCreateHorizontalPipeLink()
        {
            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 5));
            mockBlockFactory.Setup(factory => factory.CreateRenderable()).Returns(new Mock<IRenderable>().Object);
            mockPipeLinkFactory.Setup(factory => factory.CreateRenderable()).Returns(new Mock<IRenderable>().Object);
            mockBlueprint.Setup(blueprint => blueprint.HasBlock(new Coordinate(2, 4))).Returns(true);
            mockBlueprint.Setup(blueprint => blueprint.HasBlock(new Coordinate(2, 3))).Returns(true);

            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);
            var mockTile = new Mock<IBuilderWorldObject>();
            tiles.Set(new Coordinate(2, 3), mockTile.Object);
            tiles.Set(new Coordinate(2, 4), mockTile.Object);
            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 3));

            mockPipeLinkFactory.Verify(factory => factory.CreateRenderable(), Times.Once());
        }

        [TestMethod]
        public void CheckIfFactoryIsUsedToCreateVerticalPipeLink()
        {
            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 5));
            mockBlockFactory.Setup(factory => factory.CreateRenderable()).Returns(new Mock<IRenderable>().Object);
            mockPipeLinkFactory.Setup(factory => factory.CreateRenderable()).Returns(new Mock<IRenderable>().Object);
            mockBlueprint.Setup(blueprint => blueprint.HasBlock(new Coordinate(3, 3))).Returns(true);
            mockBlueprint.Setup(blueprint => blueprint.HasBlock(new Coordinate(2, 3))).Returns(true);

            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);
            var mockTile = new Mock<IBuilderWorldObject>();
            tiles.Set(new Coordinate(2, 3), mockTile.Object);
            tiles.Set(new Coordinate(3, 3), mockTile.Object);
            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 3));

            mockPipeLinkFactory.Verify(factory => factory.CreateRenderable(), Times.Once());
        }
    }
}
