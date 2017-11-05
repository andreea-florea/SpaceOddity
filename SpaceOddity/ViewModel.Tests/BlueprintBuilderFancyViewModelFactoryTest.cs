using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewInterface;
using Game.Interfaces;
using ViewModel.Interfaces;
using NaturalNumbersMath;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderFancyViewModelFactoryTest
    {
        private Mock<IViewModelTilesFactory> mockTilesFactory;
        private IWorldObject[,] tiles;
        private Mock<IWorldObject> mockTile;
        private Mock<IWorldObjectFactory> mockBlockCoreFactory;
        private Mock<IWorldObjectFactory> mockRoundCornerFactory;
        private Mock<IWorldObjectFactory> mockStraightUpCornerFactory;
        private Mock<IWorldObjectFactory> mockStraightRightCornerFactory;
        private Mock<IWorldObjectFactory> mockClosedCornerFactory;
        private Mock<IWorldObjectFactory> mockOutsideUpCornerFactory;
        private Mock<IWorldObjectFactory> mockOutsideRightCornerFactory;
        private Mock<IWorldObjectFactory> mockDiagonalMissingCornerFactory;
        private Mock<IWorldObjectFactory> mockRoundEdgeFactory;
        private Mock<IWorldObjectFactory> mockClosedEdgeFactory;

        private Mock<IObservableBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlock> mockBlock;
        private Mock<IWorldObject> mockDetail;
        private Mock<IRectangleSection> mockFittingRectangle;
        private BlueprintBuilderFancyViewModelFactory blueprintBuilderViewModelFactory; 

        [TestInitialize]
        public void Initialize()
        {
            mockBlockCoreFactory = new Mock<IWorldObjectFactory>();
            mockRoundCornerFactory = new Mock<IWorldObjectFactory>();
            mockStraightUpCornerFactory = new Mock<IWorldObjectFactory>();
            mockStraightRightCornerFactory = new Mock<IWorldObjectFactory>();
            mockClosedCornerFactory = new Mock<IWorldObjectFactory>();
            mockOutsideUpCornerFactory = new Mock<IWorldObjectFactory>();
            mockOutsideRightCornerFactory = new Mock<IWorldObjectFactory>();
            mockDiagonalMissingCornerFactory = new Mock<IWorldObjectFactory>();
            mockRoundEdgeFactory = new Mock<IWorldObjectFactory>();
            mockClosedEdgeFactory = new Mock<IWorldObjectFactory>();

            mockBlueprintBuilder = new Mock<IObservableBlueprintBuilder>();
            mockBlock = new Mock<IBlock>();
            mockDetail = new Mock<IWorldObject>();
            mockFittingRectangle = new Mock<IRectangleSection>();

            tiles = new IWorldObject[3, 4];
            mockTile = new Mock<IWorldObject>();
            mockTile.SetupAllProperties();
            mockTilesFactory = new Mock<IViewModelTilesFactory>();
            mockTilesFactory.Setup(
                factory => factory.CreateTiles(mockBlueprintBuilder.Object, mockFittingRectangle.Object))
                .Returns(tiles);

            var mockController = new Mock<IBlueprintBuilderControlAssigner>();

            blueprintBuilderViewModelFactory = new BlueprintBuilderFancyViewModelFactory(
                mockTilesFactory.Object,
                mockBlockCoreFactory.Object,
                mockRoundCornerFactory.Object,
                mockStraightUpCornerFactory.Object,
                mockStraightRightCornerFactory.Object,
                mockClosedCornerFactory.Object,
                mockOutsideUpCornerFactory.Object,
                mockOutsideRightCornerFactory.Object,
                mockDiagonalMissingCornerFactory.Object,
                mockRoundEdgeFactory.Object,
                mockClosedEdgeFactory.Object,
                mockController.Object);
        }

        [TestMethod]
        public void RoundCornerDetailsAreCreatedWithCorrectFactory()
        {
            var position = new Coordinate(2, 1);
            tiles[1, 2] = mockTile.Object;
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprintBuilder.Object, position);

            mockRoundCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(4));
        }

        [TestMethod]
        public void StraightCornerDetailsAreCreatedWithCorrectFactory()
        {
            tiles[1, 2] = mockTile.Object;
            tiles[2, 2] = mockTile.Object;
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 2))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 2))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprintBuilder.Object, new Coordinate(2, 1));

            mockStraightUpCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(2));
            mockRoundCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(2));
        }

        [TestMethod]
        public void OutsideRoundedCornerDetailsAreCreatedWithCorrectFactory()
        {
            tiles[1, 2] = mockTile.Object;
            tiles[2, 2] = mockTile.Object;
            tiles[1, 3] = mockTile.Object;
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 2))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(3, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 2))).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(3, 1))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockOutsideUpCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockOutsideRightCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockDiagonalMissingCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprintBuilder.Object, new Coordinate(2, 1));

            mockStraightUpCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(2));
            mockRoundCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(1));
            mockDiagonalMissingCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(1));
            mockOutsideUpCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(1));
            mockOutsideRightCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(1));
        }

        [TestMethod]
        public void IgnoreOnlyDiagonalBlockAndCreateRoundCorner()
        {
            tiles[1, 2] = mockTile.Object;
            tiles[2, 3] = mockTile.Object;
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(3, 2))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(3, 2))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprintBuilder.Object, new Coordinate(2, 1));

            mockRoundCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(5));
        }

        [TestMethod]
        public void CreateClosedCornerIfSurroundedByBlocks()
        {
            tiles[1, 2] = mockTile.Object;
            tiles[2, 2] = mockTile.Object;
            tiles[1, 3] = mockTile.Object;
            tiles[2, 3] = mockTile.Object;
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 2))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(3, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(3, 2))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 2))).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(3, 1))).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(3, 2))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockClosedCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprintBuilder.Object, new Coordinate(2, 1));

            mockClosedCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(4));
            mockStraightUpCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(2));
            mockRoundCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(1));
        }

        [TestMethod]
        public void RoundEdgesDetailsAreCreatedWithCorrectFactory()
        {
            tiles[1, 2] = mockTile.Object;
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprintBuilder.Object, new Coordinate(2, 1));

            mockRoundEdgeFactory.Verify(factory => factory.CreateObject(), Times.Exactly(4));
        }

        [TestMethod]
        public void ClosedEdgesDetailsAreCreatedWithCorrectFactory()
        {
            tiles[1, 2] = mockTile.Object;
            tiles[2, 2] = mockTile.Object;
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 2))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 2))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprintBuilder.Object, new Coordinate(2, 1));

            mockRoundEdgeFactory.Verify(factory => factory.CreateObject(), Times.Exactly(3));
            mockClosedEdgeFactory.Verify(factory => factory.CreateObject(), Times.Exactly(2));
        }

        [TestMethod]
        public void CenterBlockDetailIsCreatedCorrectly()
        {
            tiles.Set(new Coordinate(2, 1), mockTile.Object);
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprintBuilder.Object, new Coordinate(2, 1));

            mockBlockCoreFactory.Verify(factory => factory.CreateObject(), Times.Once());
        }

        [TestMethod]
        public void CheckIfViewModelIsAttachedAsAnObserverToBlueprintBuilder()
        {
            var blueprintBuilderViewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            mockBlueprintBuilder.Verify(builder => builder.AttachObserver(blueprintBuilderViewModel), Times.Once());
        }

        [TestMethod]
        public void CreateDiagonalMissingCornerIfSurroundedByBlocksExpectForDiagonal()
        {
            tiles.Set(new Coordinate(2, 1), mockTile.Object);
            tiles.Set(new Coordinate(2, 2), mockTile.Object);
            tiles.Set(new Coordinate(3, 1), mockTile.Object);
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 2))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(3, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 2))).Returns(mockBlock.Object);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(3, 1))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockDiagonalMissingCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockOutsideRightCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockOutsideUpCornerFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateObject()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprintBuilder.Object, new Coordinate(2, 1));

            mockDiagonalMissingCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(1));
            mockStraightUpCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(2));
            mockOutsideUpCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(1));
            mockOutsideRightCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(1));
            mockRoundCornerFactory.Verify(factory => factory.CreateObject(), Times.Exactly(1));
        }
    }
}
