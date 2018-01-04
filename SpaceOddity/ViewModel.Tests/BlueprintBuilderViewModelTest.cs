using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using ViewInterface;
using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using ViewModel.Actions;
using ViewModel.DataStructures;
using ViewModel.Controller;
using Algorithm;
using Game;
using System.Collections.Generic;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderViewModelTest
    {
        private Mock<IBuilderWorldObject> mockBlock;
        private Mock<IBuilderWorldObject> mockShipComponent;
        private Mock<IBuilderWorldObject> mockPipeLink;
        private Mock<IFactory<IBuilderWorldObject>> mockBlockFactory;
        private Mock<IFactory<IBuilderWorldObject>> mockShipComponentFactory;
        private Mock<IFactory<IBuilderWorldObject>> mockPipeLinkFactory;
        private Mock<IFactory<IWorldObject, ICurve>> mockPipeFactory;
        private Mock<IBlueprint> mockBlueprint;
        private Mock<IBlueprintBuilderControlAssigner> mockController;
        private Mock<IBuilderWorldObject> mockTile;
        private Mock<IWorldObject> mockPipe;
        private IBuilderWorldObject[,] blocks;
        private IBuilderWorldObject[,] tiles;
        private IBuilderWorldObject[,] shipComponents;
        private IBuilderWorldObject[,] horizontalPipeLinks;
        private IBuilderWorldObject[,] verticalPipeLinks;
        private Dictionary<PipePosition, IWorldObject> doubleEdgedPipes;
        private BlueprintBuilderViewModel blueprintBuilderViewModel;

        [TestInitialize]
        public void Initialize()
        {
            mockBlock = new Mock<IBuilderWorldObject>();
            mockBlock.SetupAllProperties();
            mockShipComponent = new Mock<IBuilderWorldObject>();
            mockShipComponent.SetupAllProperties();
            mockPipeLink = new Mock<IBuilderWorldObject>();
            mockPipeLink.SetupAllProperties();
            mockPipe = new Mock<IWorldObject>();
            mockPipe.SetupAllProperties();

            mockBlockFactory = new Mock<IFactory<IBuilderWorldObject>>();
            mockShipComponentFactory = new Mock<IFactory<IBuilderWorldObject>>();
            mockPipeLinkFactory = new Mock<IFactory<IBuilderWorldObject>>();
            mockPipeFactory = new Mock<IFactory<IWorldObject, ICurve>>();
            mockBlueprint = new Mock<IBlueprint>();
            mockController = new Mock<IBlueprintBuilderControlAssigner>();

            mockTile = new Mock<IBuilderWorldObject>();
            blocks = new IBuilderWorldObject[5, 6];
            tiles = new IBuilderWorldObject[5, 6];
            shipComponents = new IBuilderWorldObject[5, 6];
            horizontalPipeLinks = new IBuilderWorldObject[5, 5];
            verticalPipeLinks = new IBuilderWorldObject[4, 6];
            doubleEdgedPipes = new Dictionary<PipePosition, IWorldObject>();
            var objectTable = new BlueprintBuilderObjectTable(
                tiles, blocks, shipComponents, horizontalPipeLinks, verticalPipeLinks, doubleEdgedPipes);

            blueprintBuilderViewModel = new BlueprintBuilderViewModel(objectTable,
                    mockBlockFactory.Object, 
                    mockShipComponentFactory.Object, 
                    mockPipeLinkFactory.Object,
                    mockPipeFactory.Object,
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
            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);
            mockTile.Setup(tile => tile.Position).Returns(new Vector2(3, 6));
            mockTile.Setup(tile => tile.Scale).Returns(new Vector2(7, 5));

            tiles.Set(position, mockTile.Object);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            var coordinate = new Coordinate(2, 3);
            Assert.AreEqual(tiles[3, 2].Position, blueprintBuilderViewModel.GetBlock(coordinate).Position);
            Assert.AreEqual(tiles[3, 2].Scale, blueprintBuilderViewModel.GetBlock(coordinate).Scale);
        }

        [TestMethod]
        public void CheckIfControllIsAssignedToBlock()
        {
            var position = new Coordinate(2, 3);
            tiles.Set(position, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            mockController.Verify(controller => 
                controller.AssignBlockControl(mockBlock.Object, position), Times.Once());
        }

        [TestMethod]
        public void CheckIfObjectIsDeletedFromView()
        {
            var position = new Coordinate(2, 3);
            blocks.Set(position, mockBlock.Object);

            blueprintBuilderViewModel.BlockDeleted(mockBlueprint.Object, position);

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
            mockShipComponentFactory.Setup(factory => factory.Create()).Returns(mockShipComponent.Object);

            blueprintBuilderViewModel.ShipComponentAdded(mockBlueprint.Object, position);

            mockShipComponentFactory.Verify(factory => factory.Create(), Times.Once());
            mockController.Verify(controller => controller.AssignShipComponentControl(shipComponents.Get(position), position), Times.Once());
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
            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(mockPipeLink.Object);
            mockBlueprint.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprint.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            Assert.AreEqual(mockPipeLink.Object, verticalPipeLinks.Get(position));
        }

        [TestMethod]
        public void CheckIfPipeLinkIsCreatedBetweenTwoBlocksWhenBottomBlockExists()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 2);
            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(mockPipeLink.Object);
            mockBlueprint.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprint.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            Assert.AreEqual(mockPipeLink.Object, verticalPipeLinks.Get(connectingPosition));
        }

        [TestMethod]
        public void CheckIfPipeLinkIsCreatedWithCorrectTranslation()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 4);
            var mockTile2 = new Mock<IBuilderWorldObject>();

            mockTile.SetupGet(tile => tile.Position).Returns(new Vector2(2, 3));
            mockTile2.SetupGet(tile => tile.Position).Returns(new Vector2(2, 4));

            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile2.Object);

            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(mockPipeLink.Object);
            mockBlueprint.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprint.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            Assert.AreEqual(new Vector2(2, 3.5), verticalPipeLinks.Get(position).Position);
        }

        [TestMethod]
        public void CheckIfHorizontalPipeLinkIsNotCreatedIfBlocksAreNotConnected()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 2);
            tiles.Set(position, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(mockPipeLink.Object);
            mockBlueprint.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprint.Setup(builder => builder.HasBlock(connectingPosition)).Returns(false);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            mockPipeLinkFactory.Verify(factory => factory.Create(), Times.Never());
        }

        [TestMethod]
        public void CheckIfPipeLinkIsCreatedBetweenTwoBlocksWhenRightBlockExists()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(3, 3);
            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(mockPipeLink.Object);
            mockBlueprint.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprint.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            Assert.AreEqual(mockPipeLink.Object, horizontalPipeLinks.Get(position));
        }

        [TestMethod]
        public void CheckIfPipeLinkIsCreatedBetweenTwoBlocksWhenLeftBlockExists()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(1, 3);
            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(mockPipeLink.Object);
            mockBlueprint.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprint.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            Assert.AreEqual(mockPipeLink.Object, horizontalPipeLinks.Get(connectingPosition));
        }

        [TestMethod]
        public void CheckIfPipeLinkIsDeleted()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(1, 3);
            blocks.Set(position, mockBlock.Object);
            horizontalPipeLinks.Set(connectingPosition, mockPipeLink.Object);

            blueprintBuilderViewModel.BlockDeleted(mockBlueprint.Object, position);

            mockPipeLink.Verify(link => link.Delete(), Times.Once());
        }

        [TestMethod]
        public void CheckIfPipeLinkIsRotatedCorrectly()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 4);
            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(mockPipeLink.Object);
            mockBlueprint.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprint.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            Assert.AreEqual(new Vector2(0, 1), verticalPipeLinks.Get(position).Rotation);
        }

        [TestMethod]
        public void CheckIfControllIsAssignedtoPipeLink()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 4);
            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile.Object);
            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(mockPipeLink.Object);
            mockBlueprint.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprint.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            var edge = new CoordinatePair(position, connectingPosition);
            mockController.Verify(controller =>
                controller.AssignPipeLinkControl(mockPipeLink.Object, edge), Times.Once());
            Assert.AreEqual(new Vector2(0, 1), verticalPipeLinks.Get(position).Rotation);
        }

        [TestMethod]
        public void CreateCurvedPipeObjectAtCorrectPosition()
        {
            var position = new Coordinate(2, 3);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);

            tiles.Set(position, mockTile.Object);
            var location = new Vector2(1, 3);
            var scale = new Vector2(4, 5);
            mockTile.SetupGet(tile => tile.Position).Returns(location);
            mockTile.SetupGet(tile => tile.Scale).Returns(scale);
            mockPipeFactory.Setup(factory => factory.Create(It.IsAny<ICurve>())).Returns(mockPipe.Object);

            blueprintBuilderViewModel.DoubleEdgePipeAdded(mockBlueprint.Object, position, pipe);

            Assert.AreEqual(location, doubleEdgedPipes[new PipePosition(position, EdgeType.DOWN, EdgeType.UP)].Position);
            Assert.AreEqual(scale, doubleEdgedPipes[new PipePosition(position, EdgeType.DOWN, EdgeType.UP)].Scale);
        }

        [TestMethod]
        public void CreateCurvedPipeObjectWithCorrectCurve()
        {
            var position = new Coordinate(2, 3);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            tiles.Set(position, mockTile.Object);

            ICurve curve = null;
            mockPipeFactory.Setup(factory => factory.Create(It.IsAny<StraightLineCurve>()))
                .Returns(mockPipe.Object)
                .Callback<StraightLineCurve>(curveParam => curve = curveParam);


            blueprintBuilderViewModel.DoubleEdgePipeAdded(mockBlueprint.Object, position, pipe);
            Assert.AreEqual(new Vector2(0, -1), curve.GetPoint(0));
            Assert.AreEqual(new Vector2(0, 1), curve.GetPoint(1));
        }

        [TestMethod]
        public void CheckIfDoubleEdgedPipeIsDeletedCorrectly()
        {
            var position = new Coordinate(2, 3);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            var key = new PipePosition(position, pipe.FirstEdge, pipe.SecondEdge);

            doubleEdgedPipes.Add(key, mockPipe.Object);
            blueprintBuilderViewModel.DoubleEdgePipeDeleted(mockBlueprint.Object, position, pipe);
            Assert.IsFalse(doubleEdgedPipes.ContainsKey(key));
        }

        [TestMethod]
        public void CreateConnectingPipeCurveCorrectly()
        {
            var position = new Coordinate(2, 3);
            var pipe = new ConnectingPipe(EdgeType.DOWN);
            tiles.Set(position, mockTile.Object);

            ICurve curve = null;
            mockPipeFactory.Setup(factory => factory.Create(It.IsAny<StraightLineCurve>()))
                .Returns(mockPipe.Object)
                .Callback<StraightLineCurve>(curveParam => curve = curveParam);

            blueprintBuilderViewModel.ConnectingPipeAdded(mockBlueprint.Object, position, pipe);
            Assert.AreEqual(new Vector2(0, -1), curve.GetPoint(0));
            Assert.AreEqual(new Vector2(0, 0), curve.GetPoint(1));
        }

        [TestMethod]
        public void CheckIfConnectingPipeIsDeletedCorrectly()
        {
            var position = new Coordinate(2, 3);
            var pipe = new ConnectingPipe(EdgeType.DOWN);
            var key = new PipePosition(position, pipe.Edge, EdgeType.COUNT);

            doubleEdgedPipes.Add(key, mockPipe.Object);
            blueprintBuilderViewModel.ConnectingPipeDeleted(mockBlueprint.Object, position, pipe);
            Assert.IsFalse(doubleEdgedPipes.ContainsKey(key));
        }
    }
}
