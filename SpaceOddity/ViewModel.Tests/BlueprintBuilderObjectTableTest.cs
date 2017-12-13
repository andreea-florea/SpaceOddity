using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturalNumbersMath;
using ViewModel.DataStructures;
using Moq;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderObjectTableTest
    {
        private BlueprintBuilderObjectTable objectTable;
        private Mock<IBuilderWorldObject> mockBuilderObject;
        private IBuilderWorldObject[,] tiles;
        private IBuilderWorldObject[,] blocks;
        private IBuilderWorldObject[,] shipComponents;
        private IBuilderWorldObject[,] horizontalPipeLinks;
        private IBuilderWorldObject[,] verticalPipeLinks;

        [TestInitialize]
        public void Initialize()
        {
            mockBuilderObject = new Mock<IBuilderWorldObject>();

            tiles = new IBuilderWorldObject[5, 6];
            blocks = new IBuilderWorldObject[5, 6];
            shipComponents = new IBuilderWorldObject[5, 6];
            horizontalPipeLinks = new IBuilderWorldObject[5, 5];
            verticalPipeLinks = new IBuilderWorldObject[4, 6];

            objectTable = new BlueprintBuilderObjectTable(tiles, blocks, shipComponents, horizontalPipeLinks, verticalPipeLinks);
        }

        [TestMethod]
        public void CorrectTileIsReturned()
        {
            var position = new Coordinate(3, 2);
            tiles.Set(position, mockBuilderObject.Object);
            var tile = objectTable.GetTile(position);
            Assert.AreEqual(mockBuilderObject.Object, tile);
        }

        [TestMethod]
        public void CorrectBlockIsReturned()
        {
            var position = new Coordinate(3, 1);
            objectTable.SetBlock(position, mockBuilderObject.Object);
            var block = objectTable.GetBlock(position);
            Assert.AreEqual(mockBuilderObject.Object, block);
        }

        [TestMethod]
        public void BlockIsDeletedCorrectly()
        {
            var position = new Coordinate(1, 2);
            blocks.Set(position, mockBuilderObject.Object);
            objectTable.DeleteBlock(position);
            Assert.AreEqual(null, blocks.Get(position));
        }

        [TestMethod]
        public void CorrectShipComponentIsReturned()
        {
            var position = new Coordinate(3, 1);
            objectTable.SetShipComponent(position, mockBuilderObject.Object);
            var block = objectTable.GetShipComponent(position);
            Assert.AreEqual(mockBuilderObject.Object, block);
        }

        [TestMethod]
        public void HorizontalPipeLinkIsSetCorrectly()
        {
            var position = new Coordinate(1, 4);
            var connectingPosition = new Coordinate(2, 4);
            var edge = new CoordinatePair(position, connectingPosition);

            objectTable.SetPipeLink(edge, mockBuilderObject.Object);

            Assert.AreEqual(mockBuilderObject.Object, horizontalPipeLinks.Get(position));
        }

        [TestMethod]
        public void PipeLinkIsDeletedCorrectly()
        {
            var position = new Coordinate(1, 4);
            var connectingPosition = new Coordinate(2, 4);
            var edge = new CoordinatePair(position, connectingPosition);

            horizontalPipeLinks.Set(position, mockBuilderObject.Object);
            objectTable.DeletePipeLink(edge);
            Assert.AreEqual(null, horizontalPipeLinks.Get(position));
        }

        [TestMethod]
        public void CheckIfCorrectHorizontalPipeLinkIsReturned()
        {
            var position = new Coordinate(1, 4);
            var connectingPosition = new Coordinate(2, 4);
            var edge = new CoordinatePair(position, connectingPosition);

            horizontalPipeLinks.Set(position, mockBuilderObject.Object);
            var pipeLink = objectTable.GetPipeLink(edge);

            Assert.AreEqual(mockBuilderObject.Object, pipeLink);
        }

        [TestMethod]
        public void CheckIfCorrectVerticalPipeLinkIsReturned()
        {
            var position = new Coordinate(1, 3);
            var connectingPosition = new Coordinate(1, 4);
            var edge = new CoordinatePair(position, connectingPosition);

            verticalPipeLinks.Set(position, mockBuilderObject.Object);
            var pipeLink = objectTable.GetPipeLink(edge);

            Assert.AreEqual(mockBuilderObject.Object, pipeLink);
        }

        [TestMethod]
        public void CheckIfCorrectPositionIsSelectedForReturnedPipeLink()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 2);
            var edge = new CoordinatePair(position, connectingPosition);

            verticalPipeLinks.Set(connectingPosition, mockBuilderObject.Object);
            var pipeLink = objectTable.GetPipeLink(edge);

            Assert.AreEqual(mockBuilderObject.Object, pipeLink);
        }
    }
}
