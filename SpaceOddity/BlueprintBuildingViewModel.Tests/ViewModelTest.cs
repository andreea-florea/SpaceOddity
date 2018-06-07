using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewInterface;
using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Actions;
using BlueprintBuildingViewModel.DataStructures;
using BlueprintBuildingViewModel.Controller;
using Algorithms;
using Game;
using System.Collections.Generic;
using Game.Enums;
using ViewModel;

namespace BlueprintBuildingViewModel.Tests
{
    [TestClass]
    public class ViewModelTest
    {
        private Mock<IActivateableWorldObject> mockBlock;
        private Mock<IActivateableWorldObject> mockShipComponent;
        private Mock<IActivateableWorldObject> mockPipeLink;
        private Mock<IFactory<IActivateableWorldObject>> mockBlockFactory;
        private Mock<IFactory<IActivateableWorldObject, IShipComponent>> mockShipComponentFactory;
        private Mock<IFactory<IActivateableWorldObject>> mockPipeLinkFactory;
        private Mock<IFactory<IWorldObject, ICurve>> mockPipeFactory;
        private Mock<IBlueprintBuilder> mockBuilder;
        private Mock<IBlueprint> mockBlueprint;
        private Mock<IControlAssigner> mockController;
        private Mock<IActivateableWorldObject> mockTile;
        private Mock<IWorldObject> mockPipe;
        private IActivateableWorldObject[,] tiles;
        private WorldObjectDictionary<Coordinate, IActivateableWorldObject> blocks;
        private WorldObjectDictionary<Coordinate, IActivateableWorldObject> shipComponents;
        private WorldObjectDictionary<CoordinatePair, IActivateableWorldObject> pipeLinks;
        private WorldObjectDictionary<PipePosition, IWorldObject> doubleEdgedPipes;
        private ViewModel blueprintBuilderViewModel;

        [TestInitialize]
        public void Initialize()
        {
            mockBlock = new Mock<IActivateableWorldObject>();
            mockBlock.SetupAllProperties();
            mockShipComponent = new Mock<IActivateableWorldObject>();
            mockShipComponent.SetupAllProperties();
            mockPipeLink = new Mock<IActivateableWorldObject>();
            mockPipeLink.SetupAllProperties();
            mockPipe = new Mock<IWorldObject>();
            mockPipe.SetupAllProperties();

            mockBlockFactory = new Mock<IFactory<IActivateableWorldObject>>();
            mockShipComponentFactory = new Mock<IFactory<IActivateableWorldObject, IShipComponent>>();
            mockPipeLinkFactory = new Mock<IFactory<IActivateableWorldObject>>();
            mockPipeFactory = new Mock<IFactory<IWorldObject, ICurve>>();
            mockBuilder = new Mock<IBlueprintBuilder>();
            mockBlueprint = new Mock<IBlueprint>();
            mockController = new Mock<IControlAssigner>();

            mockTile = new Mock<IActivateableWorldObject>();
            tiles = new IActivateableWorldObject[5, 6];
            foreach (var coordinate in tiles.GetCoordinates())
            {
                tiles.Set(coordinate, new Mock<IActivateableWorldObject>().Object);
            }

            blocks = new WorldObjectDictionary<Coordinate, IActivateableWorldObject>();
            shipComponents = new WorldObjectDictionary<Coordinate, IActivateableWorldObject>();
            pipeLinks = new WorldObjectDictionary<CoordinatePair, IActivateableWorldObject>();
            doubleEdgedPipes = new WorldObjectDictionary<PipePosition, IWorldObject>();
            var objectTable = new ObjectTable(
                tiles, blocks, shipComponents, pipeLinks, doubleEdgedPipes);

            blueprintBuilderViewModel = new ViewModel(
                mockBuilder.Object,
                objectTable,
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
            blocks.Add(position, mockBlock.Object);

            blueprintBuilderViewModel.BlockDeleted(mockBlueprint.Object, position);

            mockBlock.Verify(block => block.Delete(), Times.Once());
            Assert.IsFalse(blocks.ContainsKey(position));
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

            var mockComponent = new Mock<IShipComponent>();
            mockShipComponentFactory.Setup(factory => factory.Create(mockComponent.Object)).Returns(mockShipComponent.Object);

            var mockConstBlock = new Mock<IBlock>();
            mockConstBlock.SetupAllProperties();
            mockConstBlock.SetupGet(block => block.ShipComponent).Returns(mockComponent.Object);
            mockBlueprint.Setup(blueprint => blueprint.GetBlock(position)).Returns(mockConstBlock.Object);

            blueprintBuilderViewModel.ShipComponentAdded(mockBlueprint.Object, position);

            mockShipComponentFactory.Verify(factory => factory.Create(mockComponent.Object), Times.Once());
            mockController.Verify(controller => controller.AssignShipComponentControl(shipComponents[position], position), Times.Once());
            Assert.AreEqual(translation, shipComponents[position].Position);
            Assert.AreEqual(scale, shipComponents[position].Scale);
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

            Assert.AreEqual(mockPipeLink.Object, pipeLinks[new CoordinatePair(position, connectingPosition)]);
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

            Assert.AreEqual(mockPipeLink.Object, pipeLinks[new CoordinatePair(position, connectingPosition)]);
        }

        [TestMethod]
        public void CheckIfPipeLinkIsCreatedWithCorrectTranslation()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(2, 4);
            var mockTile2 = new Mock<IActivateableWorldObject>();

            mockTile.SetupGet(tile => tile.Position).Returns(new Vector2(2, 3));
            mockTile2.SetupGet(tile => tile.Position).Returns(new Vector2(2, 4));

            tiles.Set(position, mockTile.Object);
            tiles.Set(connectingPosition, mockTile2.Object);

            mockBlockFactory.Setup(factory => factory.Create()).Returns(mockBlock.Object);
            mockPipeLinkFactory.Setup(factory => factory.Create()).Returns(mockPipeLink.Object);
            mockBlueprint.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprint.Setup(builder => builder.HasBlock(connectingPosition)).Returns(true);

            blueprintBuilderViewModel.BlockCreated(mockBlueprint.Object, position);

            Assert.AreEqual(new Vector2(2, 3.5), pipeLinks[new CoordinatePair(position, connectingPosition)].Position);
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

            Assert.AreEqual(mockPipeLink.Object, pipeLinks[new CoordinatePair(position, connectingPosition)]);
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

            Assert.AreEqual(mockPipeLink.Object, pipeLinks[new CoordinatePair(position, connectingPosition)]);
        }

        [TestMethod]
        public void CheckIfPipeLinkIsDeleted()
        {
            var position = new Coordinate(2, 3);
            var connectingPosition = new Coordinate(1, 3);
            blocks.Add(position, mockBlock.Object);
            pipeLinks[new CoordinatePair(position, connectingPosition)] = mockPipeLink.Object;

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

            Assert.AreEqual(new Vector2(0, 1), pipeLinks[new CoordinatePair(position, connectingPosition)].Rotation);
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
            Assert.AreEqual(new Vector2(0, 1), pipeLinks[new CoordinatePair(position, connectingPosition)].Rotation);
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
            mockPipeFactory.Setup(factory => factory.Create(It.IsAny<ICurve>()))
                .Returns(mockPipe.Object)
                .Callback<ICurve>(curveParam => curve = curveParam);

            blueprintBuilderViewModel.DoubleEdgePipeAdded(mockBlueprint.Object, position, pipe);
            Assert.AreEqual(new Vector2(0, -1), curve.GetPoint(0));
            Assert.AreEqual(new Vector2(0, 1), curve.GetPoint(1));
        }

        [TestMethod]
        public void CreateCurvedPipeObjectWithRoundCurve()
        {
            var position = new Coordinate(2, 3);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.RIGHT);
            tiles.Set(position, mockTile.Object);

            ICurve curve = null;
            mockPipeFactory.Setup(factory => factory.Create(It.IsAny<ICurve>()))
                .Returns(mockPipe.Object)
                .Callback<ICurve>(curveParam => curve = curveParam);

            blueprintBuilderViewModel.DoubleEdgePipeAdded(mockBlueprint.Object, position, pipe);
            Assert.AreEqual(new Vector2(1, 0), curve.GetPoint(0));
            Assert.AreEqual(new Vector2(0, -1), curve.GetPoint(1));
            Assert.AreEqual(new Vector2(1-Math.Cos(Math.PI*0.25), Math.Sin(Math.PI*0.25)-1), curve.GetPoint(0.5));
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

        [TestMethod]
        public void CheckIfShipComponentIsDeletedCorrectly()
        {
            var position = new Coordinate(1, 3);
            shipComponents.Add(position, mockShipComponent.Object);

            blueprintBuilderViewModel.ShipComponentDeleted(mockBlueprint.Object, position);

            mockShipComponent.Verify(component => component.Delete(), Times.Once());
            Assert.IsFalse(shipComponents.ContainsKey(position));
        }
    }
}
