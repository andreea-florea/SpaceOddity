using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Interfaces;
using Moq;
using ViewInterface;
using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderViewModelTest
    {
        [TestMethod]
        public void BlueprintHasCorrectTilesAssigned()
        {
            var mockTile = new Mock<IWorldObject>();
            var blocks = new IWorldObject[5, 6];
            var tiles = new IWorldObject[5, 6];
            tiles[3, 1] = mockTile.Object;

            var blueprintBuilderViewModel = new BlueprintBuilderViewModel(tiles, blocks, null);
            Assert.AreEqual(6, blueprintBuilderViewModel.Width);
            Assert.AreEqual(5, blueprintBuilderViewModel.Height);
            Assert.AreEqual(mockTile.Object, blueprintBuilderViewModel.GetTile(new Coordinate(1, 3)));
        }

        [TestMethod]
        public void BlueprintViewModelCreatesWorldObjectBlockWhenOneIsCreated()
        {
            var mockBlock = new Mock<IWorldObject>();
            mockBlock.SetupAllProperties();

            var mockBlockFactory = new Mock<IWorldObjectFactory>();            
            mockBlockFactory.Setup(factory => factory.CreateObject()).Returns(mockBlock.Object);

            var mockTile = new Mock<IWorldObject>();
            mockTile.Setup(tile => tile.Position).Returns(new Vector2(3, 6));
            mockTile.Setup(tile => tile.Scale).Returns(new Vector2(7, 5));

            var blocks = new IWorldObject[5, 6];
            var tiles = new IWorldObject[5, 6];
            tiles[3, 2] = mockTile.Object;

            var blueprintBuilderViewModel = new BlueprintBuilderViewModel(tiles, blocks, mockBlockFactory.Object);
            blueprintBuilderViewModel.BlockCreated(null, new Coordinate(2, 3));

            var coordinate = new Coordinate(2, 3);
            Assert.AreEqual(tiles[3, 2].Position, blueprintBuilderViewModel.GetBlock(coordinate).Position);
            Assert.AreEqual(tiles[3, 2].Scale, blueprintBuilderViewModel.GetBlock(coordinate).Scale);
        }

    }
}
