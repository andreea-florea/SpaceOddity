using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Fancy;
using Moq;
using Game.Interfaces;
using Geometry;
using ViewInterface;
using System.Collections.Generic;
using NaturalNumbersMath;
using ViewModel.Interfaces;
using ViewModel.Fancy.Iternal;

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
        private Mock<IBlueprintBuilderControlAssigner> mockController;
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
            mockController = new Mock<IBlueprintBuilderControlAssigner>();
            detailUpdates = new List<FacingPosition>();
            tiles = new IWorldObject[4, 4];
            details = new IWorldObject[4, 4];

            blockDetailsViewUpdater = new BlockDetailsViewUpdater(mockBlueprintBuilder.Object,
                tiles, details, mockFactory.Object, mockController.Object, detailUpdates);
        }

        [TestMethod]
        public void ObjectIsCreatedAtCorrectPositionScaleAndRotation()
        {
            var position = new Coordinate(1, 3);
            detailUpdates.Add(new FacingPosition(new Coordinate(1, 0), new Coordinate(0, 0)));
            mockFactory.Setup(factory => factory.CreateObject(position, new Coordinate(1, 0))).Returns(mockObject.Object);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);
            mockTile.Setup(tile => tile.Position).Returns(new Vector2(5, 7));
            mockTile.Setup(tile => tile.Scale).Returns(new Vector2(3, 2));
            tiles[3, 1] = mockTile.Object;
            
            blockDetailsViewUpdater.UpdateDetails(position);

            mockBlueprintBuilder.Verify(builder => builder.HasBlock(position), Times.Once);
            mockFactory.Verify(factory => factory.CreateObject(position, new Coordinate(1, 0)), Times.Once);
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
            tiles.Set(new Coordinate(2, 3), mockTile.Object);
            var position = new Coordinate(1, 4);
            
            blockDetailsViewUpdater.UpdateDetails(position);
            mockBlueprintBuilder.Verify(builder => builder.HasBlock(new Coordinate(2, 3)), Times.Once);
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
        public void OldDetailIsDeletedOnUpdated()
        {
            mockFactory.Setup(factory => factory.CreateObject(new Coordinate(1, 3), Coordinates.Down)).Returns(mockObject.Object);
            detailUpdates.Add(new FacingPosition(Coordinates.Down, new Coordinate(0, 0)));
            detailUpdates.Add(new FacingPosition(Coordinates.Down, new Coordinate(0, 0)));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(1, 3))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(1, 3))).Returns(mockBlock.Object);

            var position = new Coordinate(1, 3);
            tiles.Set(position, mockTile.Object);
            
            blockDetailsViewUpdater.UpdateDetails(position);

            mockFactory.Verify(factory => factory.CreateObject(new Coordinate(1, 3), Coordinates.Down), Times.Exactly(2));
            mockObject.Verify(worldObject => worldObject.Delete(), Times.Once());
        }

        [TestMethod]
        public void DetailIsDeletedIfBlockIsMissing()
        {
            detailUpdates.Add(new FacingPosition(Coordinates.Down, Coordinates.Zero));
            var position = new Coordinate(1, 3);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(false);

            var mockDetail = new Mock<IWorldObject>();
            details.Set(position, mockDetail.Object);

            blockDetailsViewUpdater.UpdateDetails(position);

            mockDetail.Verify(detail => detail.Delete(), Times.Once());
        }

        [TestMethod]
        public void DetailNotDeletedIfCallIsOutsideBounds()
        {
            detailUpdates.Add(new FacingPosition(Coordinates.Down, Coordinates.Zero));
            var position = new Coordinate(-1, 3);

            blockDetailsViewUpdater.UpdateDetails(position);
        }

        [TestMethod]
        public void BlockDetailIsAddedControl()
        {
            var position = new Coordinate(2, 3);
            mockFactory.Setup(factory => factory.CreateObject(position, Coordinates.Up)).Returns(mockObject.Object);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(true);
            detailUpdates.Add(new FacingPosition(Coordinates.Up, Coordinates.Zero));
            tiles.Set(position, mockTile.Object);

            blockDetailsViewUpdater.UpdateDetails(position);
            mockController.Verify(
                controller => controller.AssignBlockControl(mockObject.Object, position), Times.Once);       
        }
    }
}
