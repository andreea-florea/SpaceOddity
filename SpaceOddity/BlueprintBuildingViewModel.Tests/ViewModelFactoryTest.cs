using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;
using Geometry;
using System.Collections.Generic;
using ViewInterface;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Actions;
using BlueprintBuildingViewModel.Controller;
using ViewModel;
using Algorithms;

namespace BlueprintBuildingViewModel.Tests
{
    [TestClass]
    public class ViewModelFactoryTest
    {
        private Mock<IViewModelTilesFactory> mockTileFactory;
        private Mock<IFactory<IRenderable>> mockBlockFactory;
        private Mock<IFactory<IRenderable>> mockEmptyComponentsFactory;
        private Mock<IFactory<IRenderable>> mockBatteryFactory;
        private Mock<IFactory<IRenderable>> mockPipeLinkFactory;
        private Mock<IFactory<IRenderable, ICurve>> mockPipeFactory;
        private IActivateableWorldObject[,] tiles;
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprint> mockBlueprint;
        private ViewModelFactory viewModelFactory;

        [TestInitialize]
        public void Initialize()
        {
            mockTileFactory = new Mock<IViewModelTilesFactory>();
            mockBlockFactory = new Mock<IFactory<IRenderable>>();
            mockEmptyComponentsFactory = new Mock<IFactory<IRenderable>>();
            mockBatteryFactory = new Mock<IFactory<IRenderable>>();
            mockPipeLinkFactory = new Mock<IFactory<IRenderable>>();
            mockPipeFactory = new Mock<IFactory<IRenderable, ICurve>>();

            var mockEmptyTile = new Mock<IActivateableWorldObject>();
            tiles = new IActivateableWorldObject[6, 4];
            var rect = new CoordinateRectangle(Coordinates.Zero, new Coordinate(4, 6));
            foreach (var coordinate in rect.Points)
            {
                tiles.Set(coordinate, mockEmptyTile.Object);
            }

            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockBlueprint = new Mock<IBlueprint>();
            mockTileFactory.Setup(factory => 
                factory.CreateTiles(It.IsAny<Coordinate>(), It.IsAny<IRectangleSection>())).Returns(tiles);

            viewModelFactory = new ViewModelFactory(
                mockTileFactory.Object,
                mockBlockFactory.Object, 
                mockBatteryFactory.Object,
                mockEmptyComponentsFactory.Object,
                mockPipeLinkFactory.Object,
                mockPipeFactory.Object);
        }

        [TestMethod]
        public void BlueprintBuilderViewModelIsCreatedWithCorrectEmptySlots()
        {
            var position = new Coordinate(2, 3);
            var mockTile = new Mock<IActivateableWorldObject>();
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
            mockRenderable.SetupSet(block => block.RightClickAction = It.IsAny<BlockSelect>()).Verifiable();
            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockRenderable.Object);
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 6));
            var blueprintBuilderViewModel = 
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);

            var position = new Coordinate(1, 1);
            tiles.Set(position, new Mock<IActivateableWorldObject>().Object);
            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);
            blueprintBuilderViewModel.GetBlock(position);
            mockRenderable.VerifySet(block => block.RightClickAction = It.IsAny<BlockCancel>());
        }

        [TestMethod]
        public void CheckIfFactoryIsUsedToCreateShipComponents()
        {
            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 6));
            mockEmptyComponentsFactory.Setup(factory => factory.Create()).Returns(new Mock<IRenderable>().Object);

            var mockComponent = new Mock<IShipComponent>();
            var mockConstBlock = new Mock<IBlock>();
            mockConstBlock.SetupAllProperties();
            mockConstBlock.SetupGet(block => block.ShipComponent).Returns(mockComponent.Object);
            mockBlueprint.Setup(blueprint => blueprint.GetBlock(new Coordinate(2, 3))).Returns(mockConstBlock.Object);

            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);
            var mockTile = new Mock<IActivateableWorldObject>();
            tiles.Set(new Coordinate(2, 3), mockTile.Object);
            blueprintBuilderViewModel.ShipComponentAdded(mockBlueprint.Object, new Coordinate(2, 3));

            mockEmptyComponentsFactory.Verify(factory => factory.Create(), Times.Once());
        }

        [TestMethod]
        public void CheckIfFactoryIsUsedToCreateHorizontalPipeLink()
        {
            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 5));
            mockBlockFactory.Setup(factory => factory.Create()).Returns(new Mock<IRenderable>().Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(new Mock<IRenderable>().Object);
            mockBlueprint.Setup(blueprint => blueprint.HasBlock(new Coordinate(2, 4))).Returns(true);
            mockBlueprint.Setup(blueprint => blueprint.HasBlock(new Coordinate(2, 3))).Returns(true);

            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);
            var mockTile = new Mock<IActivateableWorldObject>();
            tiles.Set(new Coordinate(2, 3), mockTile.Object);
            tiles.Set(new Coordinate(2, 4), mockTile.Object);
            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 3));

            mockPipeLinkFactory.Verify(factory => factory.Create(), Times.Once());
        }

        [TestMethod]
        public void CheckIfFactoryIsUsedToCreateVerticalPipeLink()
        {
            var rectangle = new Mock<IRectangleSection>();
            rectangle.Setup(rect => rect.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(5, 8)));
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 5));
            mockBlockFactory.Setup(factory => factory.Create()).Returns(new Mock<IRenderable>().Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(new Mock<IRenderable>().Object);
            mockBlueprint.Setup(blueprint => blueprint.HasBlock(new Coordinate(3, 3))).Returns(true);
            mockBlueprint.Setup(blueprint => blueprint.HasBlock(new Coordinate(2, 3))).Returns(true);

            var blueprintBuilderViewModel =
                viewModelFactory.CreateViewModel(mockBlueprintBuilder.Object, rectangle.Object);
            var mockTile = new Mock<IActivateableWorldObject>();
            tiles.Set(new Coordinate(2, 3), mockTile.Object);
            tiles.Set(new Coordinate(3, 3), mockTile.Object);
            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 3));

            mockPipeLinkFactory.Verify(factory => factory.Create(), Times.Once());
        }
    }
}
