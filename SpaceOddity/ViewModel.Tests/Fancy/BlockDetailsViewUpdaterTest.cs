using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Fancy;
using Moq;
using Game.Interfaces;
using Geometry;
using ViewInterface;
using System.Collections.Generic;
using NaturalNumbersMath;

namespace ViewModel.Tests.Fancy
{
    [TestClass]
    public class BlockDetailsViewUpdaterTest
    {
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlock> mockBlock;
        private Mock<IWorldObject> mockTile;
        private Mock<IWorldObject> mockObject;
        private Mock<IFacingContextWorldObjectFactory> mockFactory;
        private BlockDetailsViewUpdater blockDetailsViewUpdater;
        private List<FacingPosition> detailUpdates;
        private IWorldObject[,] tiles;
        private IWorldObject[,] details;

        [TestInitialize]
        public void Initialize()
        {
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockObject = new Mock<IWorldObject>();
            mockObject.SetupAllProperties();
            mockBlock = new Mock<IBlock>();
            mockTile = new Mock<IWorldObject>();
            mockFactory = new Mock<IFacingContextWorldObjectFactory>();
            detailUpdates = new List<FacingPosition>();
            tiles = new IWorldObject[4, 4];
            details = new IWorldObject[4, 4];

            blockDetailsViewUpdater = new BlockDetailsViewUpdater(mockBlueprintBuilder.Object,
                tiles, details, mockFactory.Object, detailUpdates);
        }

        [TestMethod]
        public void ObjectIsCreatedAtCorrectPositionScaleAndRotation()
        {
            detailUpdates.Add(new FacingPosition(new Coordinate(1, 0), new Coordinate(0, 0)));
            mockFactory.Setup(factory => factory.CreateObject(new Coordinate(1, 3), new Coordinate(1, 0))).Returns(mockObject.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(3, 1)).Returns(mockBlock.Object);
            mockTile.Setup(tile => tile.Position).Returns(new Vector2(5, 7));
            mockTile.Setup(tile => tile.Scale).Returns(new Vector2(3, 2));
            tiles[3, 1] = mockTile.Object;
            var position = new Coordinate(1, 3);
            
            blockDetailsViewUpdater.UpdateDetails(position);

            mockBlueprintBuilder.Verify(builder => builder.GetBlock(3, 1), Times.Once);
            mockFactory.Verify(factory => factory.CreateObject(new Coordinate(1, 3), new Coordinate(1, 0)), Times.Once);
            Assert.AreEqual(5, mockObject.Object.Position.X);
            Assert.AreEqual(7, mockObject.Object.Position.Y);
            Assert.AreEqual(3, mockObject.Object.Scale.X);
            Assert.AreEqual(2, mockObject.Object.Scale.Y);
            Assert.AreEqual(1, mockObject.Object.Rotation.X);
            Assert.AreEqual(0, mockObject.Object.Rotation.Y);
        }

        [TestMethod]
        public void DetailIsUpdatedAtCorrectPosition()
        {
            mockFactory.Setup(factory => factory.CreateObject(new Coordinate(2, 3), Coordinates.Up)).Returns(mockObject.Object);
            detailUpdates.Add(new FacingPosition(Coordinates.Up, new Coordinate(1, -1)));
            tiles[3, 2] = mockTile.Object;
            var position = new Coordinate(1, 4);
            
            blockDetailsViewUpdater.UpdateDetails(position);
            mockBlueprintBuilder.Verify(builder => builder.GetBlock(3, 2), Times.Once);
        }

        [TestMethod]
        public void DetailIsNotCreatedForNullBlocks()
        {
            mockFactory.Setup(factory => factory.CreateObject(new Coordinate(2, 3), Coordinates.Up)).Returns(mockObject.Object);
            detailUpdates.Add(new FacingPosition(Coordinates.Up, new Coordinate(1, -1)));
            tiles[3, 2] = mockTile.Object;
            var position = new Coordinate(1, 4);
            
            blockDetailsViewUpdater.UpdateDetails(position);
            mockFactory.Verify(factory => factory.CreateObject(It.IsAny<Coordinate>(), It.IsAny<Coordinate>()), Times.Never);
        }

        [TestMethod]
        public void OldDetailIsDeletedWhenNewOneIsUpdated()
        {
            mockFactory.Setup(factory => factory.CreateObject(new Coordinate(1, 3), Coordinates.Down)).Returns(mockObject.Object);
            detailUpdates.Add(new FacingPosition(Coordinates.Down, new Coordinate(0, 0)));
            detailUpdates.Add(new FacingPosition(Coordinates.Down, new Coordinate(0, 0)));
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(3, 1)).Returns(mockBlock.Object);
            tiles[3, 1] = mockTile.Object;
            var position = new Coordinate(1, 3);
            
            blockDetailsViewUpdater.UpdateDetails(position);

            mockFactory.Verify(factory => factory.CreateObject(new Coordinate(1, 3), Coordinates.Down), Times.Exactly(2));
            mockObject.Verify(worldObject => worldObject.Delete(), Times.Once());
        }
    }
}
