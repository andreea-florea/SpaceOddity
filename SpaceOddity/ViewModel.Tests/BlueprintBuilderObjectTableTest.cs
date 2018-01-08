﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturalNumbersMath;
using ViewModel.DataStructures;
using Moq;
using Game.Enums;
using System.Collections.Generic;

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
        private Dictionary<CoordinatePair, IBuilderWorldObject> pipeLinks;
        private Dictionary<PipePosition, IWorldObject> doubleEdgedPipes;

        [TestInitialize]
        public void Initialize()
        {
            mockBuilderObject = new Mock<IBuilderWorldObject>();

            tiles = new IBuilderWorldObject[5, 6];
            blocks = new IBuilderWorldObject[5, 6];
            shipComponents = new IBuilderWorldObject[5, 6];
            pipeLinks = new Dictionary<CoordinatePair, IBuilderWorldObject>();
            doubleEdgedPipes = new Dictionary<PipePosition, IWorldObject>();

            objectTable = new BlueprintBuilderObjectTable(
                tiles, blocks, shipComponents, pipeLinks, doubleEdgedPipes);
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
        public void ShipComponentIsDeletedCorrectly()
        {
            var position = new Coordinate(1, 2);
            shipComponents.Set(position, mockBuilderObject.Object);
            objectTable.DeleteShipComponent(position);
            Assert.AreEqual(null, shipComponents.Get(position));
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

            Assert.AreEqual(mockBuilderObject.Object, pipeLinks[new CoordinatePair(position, connectingPosition)]);
        }

        [TestMethod]
        public void PipeLinkIsDeletedCorrectly()
        {
            var position = new Coordinate(1, 4);
            var connectingPosition = new Coordinate(2, 4);
            var edge = new CoordinatePair(position, connectingPosition);

            pipeLinks[new CoordinatePair(position, connectingPosition)] = mockBuilderObject.Object;
            objectTable.DeletePipeLink(edge);
            Assert.IsFalse(pipeLinks.ContainsKey(new CoordinatePair(position, connectingPosition)));
        }

        [TestMethod]
        public void CheckIfCorrectHorizontalPipeLinkIsReturned()
        {
            var position = new Coordinate(1, 4);
            var connectingPosition = new Coordinate(2, 4);
            var edge = new CoordinatePair(position, connectingPosition);

            pipeLinks[new CoordinatePair(position, connectingPosition)] = mockBuilderObject.Object;
            var pipeLink = objectTable.GetPipeLink(edge);

            Assert.AreEqual(mockBuilderObject.Object, pipeLink);
        }

        [TestMethod]
        public void CheckIfCorrectVerticalPipeLinkIsReturned()
        {
            var position = new Coordinate(1, 3);
            var connectingPosition = new Coordinate(1, 4);
            var edge = new CoordinatePair(position, connectingPosition);

            pipeLinks[new CoordinatePair(position, connectingPosition)] = mockBuilderObject.Object;
            var pipeLink = objectTable.GetPipeLink(edge);

            Assert.AreEqual(mockBuilderObject.Object, pipeLink);
        }

        [TestMethod]
        public void CheckIfCorrectPositionIsSelectedForReturnedPipeLink()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 2);
            var edge = new CoordinatePair(position, connectingPosition);

            pipeLinks[new CoordinatePair(position, connectingPosition)] = mockBuilderObject.Object;
            var pipeLink = objectTable.GetPipeLink(edge);

            Assert.AreEqual(mockBuilderObject.Object, pipeLink);
        }

        [TestMethod]
        public void CheckIfPipeIsSetCorrectly()
        {
            var position = new Coordinate(1, 4);

            objectTable.SetPipe(position, EdgeType.LEFT, EdgeType.RIGHT, mockBuilderObject.Object);

            var pipePosition = new PipePosition(position, EdgeType.LEFT, EdgeType.RIGHT);
            Assert.AreEqual(mockBuilderObject.Object, doubleEdgedPipes[pipePosition]);
        }

        [TestMethod]
        public void CheckIfPipeIsReturnedCorrectly()
        {
            var position = new Coordinate(1, 4);

            objectTable.SetPipe(position, EdgeType.LEFT, EdgeType.RIGHT, mockBuilderObject.Object);

            Assert.AreEqual(mockBuilderObject.Object, objectTable.GetPipe(position, EdgeType.LEFT, EdgeType.RIGHT));
        }

        [TestMethod]
        public void CheckIfPipeIsReturnedCorrectlyForReversePipe()
        {
            var position = new Coordinate(1, 4);

            objectTable.SetPipe(position, EdgeType.LEFT, EdgeType.RIGHT, mockBuilderObject.Object);

            Assert.AreEqual(mockBuilderObject.Object, objectTable.GetPipe(position, EdgeType.RIGHT, EdgeType.LEFT));
        }

        [TestMethod]
        public void CheckIfPipeIsDeletedCorrectly()
        {
            var position = new Coordinate(1, 4);
            var key = new PipePosition(position, EdgeType.LEFT, EdgeType.RIGHT);

            doubleEdgedPipes.Add(key, mockBuilderObject.Object);
            objectTable.DeletePipe(position, EdgeType.LEFT, EdgeType.RIGHT);

            Assert.IsFalse(doubleEdgedPipes.ContainsKey(key));
            mockBuilderObject.Verify(mock => mock.Delete(), Times.Once());
        }
    }
}
