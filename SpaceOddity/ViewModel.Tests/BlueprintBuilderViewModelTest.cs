using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using ViewInterface;
using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using ViewModel.Actions;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderViewModelTest
    {
        private Mock<IWorldObject> mockBlock;
        private Mock<IWorldObject> mockShipComponent;
        private Mock<IWorldObject> mockPipeLink;
        private Mock<IWorldObjectFactory> mockBlockFactory;
        private Mock<IWorldObjectFactory> mockShipComponentFactory;
        private Mock<IWorldObjectFactory> mockPipeLinkFactory;
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprintBuilderControlAssigner> mockController;
        private Mock<IWorldObject> mockTile;
        private IWorldObject[,] blocks;
        private IWorldObject[,] tiles;
        private IWorldObject[,] shipComponents;
        private IWorldObject[,] horizontalPipeLinks;
        private IWorldObject[,] verticalPipeLinks;
        private BlueprintBuilderViewModel blueprintBuilderViewModel;

        [TestInitialize]
        public void Initialize()
        {
            mockBlock = new Mock<IWorldObject>();
            mockBlock.SetupAllProperties();
            mockShipComponent = new Mock<IWorldObject>();
            mockShipComponent.SetupAllProperties();
            mockPipeLink = new Mock<IWorldObject>();
            mockPipeLink.SetupAllProperties();

            mockBlockFactory = new Mock<IWorldObjectFactory>();
            mockShipComponentFactory = new Mock<IWorldObjectFactory>();
            mockPipeLinkFactory = new Mock<IWorldObjectFactory>();
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockController = new Mock<IBlueprintBuilderControlAssigner>();

            mockTile = new Mock<IWorldObject>();
            blocks = new IWorldObject[5, 6];
            tiles = new IWorldObject[5, 6];
            shipComponents = new IWorldObject[5, 6];
            horizontalPipeLinks = new IWorldObject[5, 5];
            verticalPipeLinks = new IWorldObject[4, 6];

            blueprintBuilderViewModel =
                new BlueprintBuilderViewModel(tiles, blocks, shipComponents, 
                    horizontalPipeLinks, verticalPipeLinks,
                    mockBlockFactory.Object, mockShipComponentFactory.Object, mockPipeLinkFactory.Object,
                    mockController.Object);
        }

        [TestMethod]
        public void BlueprintHasCorrectTilesAssigned()
        {
            var position = new Coordinate(1, 3);
            tiles.Set(position, mockTile.Object);

            Assert.AreEqual(mockTile.Object, blueprintBuilderViewModel.GetTile(position));
        }

        [TestMethod]
        public void BlueprintViewModelCreatesWorldObjectBlockWhenOneIsCreated()
        {
            var position = new Coordinate(2, 3);
            mockBlockFactory.Setup(factory => factory.CreateObject()).Returns(mockBlock.Object);
            mockTile.Setup(tile => tile.Position).Returns(new Vector2(3, 6));
            mockTile.Setup(tile => tile.Scale).Returns(new Vector2(7, 5));

            tiles.Set(position, mockTile.Object);

            blueprintBuilderViewModel.BlockCreated(mockBlueprintBuilder.Object, position);

            var coordinate = new Coordinate(2, 3);
            Assert.AreEqual(tiles[3, 2].Position, blueprintBuilderViewModel.GetBlock(coordinate).Position);
            Assert.AreEqual(tiles[3, 2].Scale, blueprintBuilderViewModel.GetBlock(coordinate).Scale);
        }

        [TestMethod]
        public void CheckIfControllIsAssignedToBlock()
        {
            var position = new Coordinate(2, 3);
            tiles.Set(position, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.CreateObject()).Returns(mockBlock.Object);

            blueprintBuilderViewModel.BlockCreated(mockBlueprintBuilder.Object, position);

            mockController.Verify(controller => 
                controller.AssignBlockControl(mockBlock.Object, position), Times.Once());
        }

        [TestMethod]
        public void CheckIfObjectIsDeletedFromView()
        {
            var position = new Coordinate(2, 3);
            blocks.Set(position, mockBlock.Object);

            blueprintBuilderViewModel.BlockDeleted(mockBlueprintBuilder.Object, position);

            mockBlock.Verify(block => block.Delete(), Times.Once());
            Assert.AreEqual(null, blocks.Get(position));
        }

        [TestMethod]
        public void CheckIfShipComponentIsAddedAtCorrectPositionAndScale()
        {
            var position = new Coordinate(2, 3);
            var translation = new Vector2(4, 5);
            var scale = new Vector2(3, 2);
            tiles.Set(position, mockTile.Object);
            mockTile.SetupGet(tile => tile.Position).Returns(translation);
            mockTile.SetupGet(tile => tile.Scale).Returns(scale);
            mockShipComponentFactory.Setup(factory => factory.CreateObject()).Returns(mockShipComponent.Object);

            blueprintBuilderViewModel.ShipComponentAdded(mockBlueprintBuilder.Object, position);

            mockShipComponentFactory.Verify(factory => factory.CreateObject(), Times.Once());
            Assert.AreEqual(translation, shipComponents.Get(position).Position);
            Assert.AreEqual(scale, shipComponents.Get(position).Scale);
        }


        [TestMethod]
        public void CheckIfPipeLinkIsCreatedBetweenTwoBlocksWhenTopBlockExists()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 4);
            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.CreateObject()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.CreateObject()).Returns(mockPipeLink.Object);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprintBuilder.Object, position);

            Assert.AreEqual(mockPipeLink.Object, horizontalPipeLinks.Get(position));
        }

        [TestMethod]
        public void CheckIfPipeLinkIsCreatedBetweenTwoBlocksWhenBottomBlockExists()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 2);
            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.CreateObject()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.CreateObject()).Returns(mockPipeLink.Object);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprintBuilder.Object, position);

            Assert.AreEqual(mockPipeLink.Object, horizontalPipeLinks.Get(connectingPosition));
        }

        [TestMethod]
        public void CheckIfPipeLinkIsCreatedWithCorrectTranslation()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 4);
            var mockTile2 = new Mock<IWorldObject>();

            mockTile.SetupGet(tile => tile.Position).Returns(new Vector2(2, 3));
            mockTile2.SetupGet(tile => tile.Position).Returns(new Vector2(2, 4));

            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile2.Object);

            mockBlockFactory.Setup(factory => factory.CreateObject()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.CreateObject()).Returns(mockPipeLink.Object);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprintBuilder.Object, position);

            Assert.AreEqual(new Vector2(2, 3.5), horizontalPipeLinks.Get(position).Position);
        }

        [TestMethod]
        public void CheckIfHorizontalPipeLinkIsNotCreatedIfBlocksAreNotConnected()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 2);
            tiles.Set(position, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.CreateObject()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.CreateObject()).Returns(mockPipeLink.Object);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(connectingPosition)).Returns(false);


            blueprintBuilderViewModel.BlockCreated(mockBlueprintBuilder.Object, position);

            mockPipeLinkFactory.Verify(factory => factory.CreateObject(), Times.Never());
        }

        [TestMethod]
        public void CheckIfPipeLinkIsCreatedBetweenTwoBlocksWhenRightBlockExists()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(3, 3);
            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.CreateObject()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.CreateObject()).Returns(mockPipeLink.Object);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprintBuilder.Object, position);

            Assert.AreEqual(mockPipeLink.Object, verticalPipeLinks.Get(position));
        }

        [TestMethod]
        public void CheckIfPipeLinkIsCreatedBetweenTwoBlocksWhenLeftBlockExists()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(1, 3);
            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.CreateObject()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.CreateObject()).Returns(mockPipeLink.Object);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprintBuilder.Object, position);

            Assert.AreEqual(mockPipeLink.Object, verticalPipeLinks.Get(connectingPosition));
        }

        [TestMethod]
        public void CheckIfPipeLinkIsDeleted()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(1, 3);
            blocks.Set(position, mockBlock.Object);
            verticalPipeLinks.Set(connectingPosition, mockPipeLink.Object);

            blueprintBuilderViewModel.BlockDeleted(mockBlueprintBuilder.Object, position);

            mockPipeLink.Verify(link => link.Delete(), Times.Once());
        }
    }
}
