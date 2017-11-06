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
        private Mock<IWorldObjectFactory> mockBlockFactory;
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprintBuilderControlAssigner> mockController;
        private Mock<IWorldObject> mockTile;
        private IWorldObject[,] blocks;
        private IWorldObject[,] tiles;
        private BlueprintBuilderViewModel blueprintBuilderViewModel;

        [TestInitialize]
        public void Initialize()
        {
            mockBlock = new Mock<IWorldObject>();
            mockBlock.SetupAllProperties();

            mockBlockFactory = new Mock<IWorldObjectFactory>();
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockController = new Mock<IBlueprintBuilderControlAssigner>();

            mockTile = new Mock<IWorldObject>();
            blocks = new IWorldObject[5, 6];
            tiles = new IWorldObject[5, 6];

            blueprintBuilderViewModel =
                new BlueprintBuilderViewModel(tiles, blocks, mockBlockFactory.Object, mockController.Object);
        }

        [TestMethod]
        public void BlueprintHasCorrectTilesAssigned()
        {
            var position = new Coordinate(1, 3);
            tiles.Set(position, mockTile.Object);

            Assert.AreEqual(6, blueprintBuilderViewModel.Width);
            Assert.AreEqual(5, blueprintBuilderViewModel.Height);
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
    }
}
