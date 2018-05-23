using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewInterface;
using Game.Interfaces;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Controller;
using BlueprintBuildingViewModel.Fancy;
using ViewModel;
using Algorithms;

namespace BlueprintBuildingViewModel.Tests
{
    [TestClass]
    public class FancyViewModelFactoryTest
    {
        private Mock<IViewModelTilesFactory> mockTilesFactory;
        private IActivateableWorldObject[,] tiles;
        private Mock<IActivateableWorldObject> mockTile;
        private Mock<IFactory<IRenderable>> mockBlockCoreFactory;
        private Mock<IFactory<IRenderable>> mockRoundCornerFactory;
        private Mock<IFactory<IRenderable>> mockStraightUpCornerFactory;
        private Mock<IFactory<IRenderable>> mockStraightRightCornerFactory;
        private Mock<IFactory<IRenderable>> mockClosedCornerFactory;
        private Mock<IFactory<IRenderable>> mockOutsideUpCornerFactory;
        private Mock<IFactory<IRenderable>> mockOutsideRightCornerFactory;
        private Mock<IFactory<IRenderable>> mockDiagonalMissingCornerFactory;
        private Mock<IFactory<IRenderable>> mockRoundEdgeFactory;
        private Mock<IFactory<IRenderable>> mockClosedEdgeFactory;

        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprint> mockBlueprint;
        private Mock<IBlock> mockBlock;
        private Mock<IRenderable> mockDetail;
        private Mock<IRectangleSection> mockFittingRectangle;
        private FancyViewModelFactory blueprintBuilderViewModelFactory; 

        [TestInitialize]
        public void Initialize()
        {
            mockBlockCoreFactory = new Mock<IFactory<IRenderable>>();
            mockRoundCornerFactory = new Mock<IFactory<IRenderable>>();
            mockStraightUpCornerFactory = new Mock<IFactory<IRenderable>>();
            mockStraightRightCornerFactory = new Mock<IFactory<IRenderable>>();
            mockClosedCornerFactory = new Mock<IFactory<IRenderable>>();
            mockOutsideUpCornerFactory = new Mock<IFactory<IRenderable>>();
            mockOutsideRightCornerFactory = new Mock<IFactory<IRenderable>>();
            mockDiagonalMissingCornerFactory = new Mock<IFactory<IRenderable>>();
            mockRoundEdgeFactory = new Mock<IFactory<IRenderable>>();
            mockClosedEdgeFactory = new Mock<IFactory<IRenderable>>();

            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockBlueprint = new Mock<IBlueprint>();
            mockBlock = new Mock<IBlock>();
            mockDetail = new Mock<IRenderable>();
            mockFittingRectangle = new Mock<IRectangleSection>();

            tiles = new IActivateableWorldObject[3, 4];
            mockTile = new Mock<IActivateableWorldObject>();
            mockTile.SetupAllProperties();
            mockTilesFactory = new Mock<IViewModelTilesFactory>();
            mockTilesFactory.Setup(
                factory => factory.CreateTiles(new Coordinate(4, 3), mockFittingRectangle.Object))
                .Returns(tiles);

            blueprintBuilderViewModelFactory = new FancyViewModelFactory(
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
                mockClosedEdgeFactory.Object);
        }

        [TestMethod]
        public void RoundCornerDetailsAreCreatedWithCorrectFactory()
        {
            var position = new Coordinate(2, 1);
            tiles[1, 2] = mockTile.Object;
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(position)).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(position)).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, position);

            mockRoundCornerFactory.Verify(factory => factory.Create(), Times.Exactly(4));
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
            mockRoundCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockStraightUpCornerFactory.Verify(factory => factory.Create(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.Create(), Times.Exactly(2));
            mockRoundCornerFactory.Verify(factory => factory.Create(), Times.Exactly(2));
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
            mockRoundCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockOutsideUpCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockOutsideRightCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockDiagonalMissingCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockStraightUpCornerFactory.Verify(factory => factory.Create(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.Create(), Times.Exactly(2));
            mockRoundCornerFactory.Verify(factory => factory.Create(), Times.Exactly(1));
            mockDiagonalMissingCornerFactory.Verify(factory => factory.Create(), Times.Exactly(1));
            mockOutsideUpCornerFactory.Verify(factory => factory.Create(), Times.Exactly(1));
            mockOutsideRightCornerFactory.Verify(factory => factory.Create(), Times.Exactly(1));
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
            mockRoundCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockRoundCornerFactory.Verify(factory => factory.Create(), Times.Exactly(5));
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
            mockRoundCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockClosedCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockClosedCornerFactory.Verify(factory => factory.Create(), Times.Exactly(4));
            mockStraightUpCornerFactory.Verify(factory => factory.Create(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.Create(), Times.Exactly(2));
            mockRoundCornerFactory.Verify(factory => factory.Create(), Times.Exactly(1));
        }

        [TestMethod]
        public void RoundEdgesDetailsAreCreatedWithCorrectFactory()
        {
            tiles[1, 2] = mockTile.Object;
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockRoundEdgeFactory.Verify(factory => factory.Create(), Times.Exactly(4));
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
            mockRoundCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockRoundEdgeFactory.Verify(factory => factory.Create(), Times.Exactly(3));
            mockClosedEdgeFactory.Verify(factory => factory.Create(), Times.Exactly(2));
        }

        [TestMethod]
        public void CenterBlockDetailIsCreatedCorrectly()
        {
            tiles.Set(new Coordinate(2, 1), mockTile.Object);
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockBlockCoreFactory.Verify(factory => factory.Create(), Times.Once());
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
            mockRoundCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockDiagonalMissingCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockOutsideRightCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockOutsideUpCornerFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.Create()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockDiagonalMissingCornerFactory.Verify(factory => factory.Create(), Times.Exactly(1));
            mockStraightUpCornerFactory.Verify(factory => factory.Create(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.Create(), Times.Exactly(2));
            mockOutsideUpCornerFactory.Verify(factory => factory.Create(), Times.Exactly(1));
            mockOutsideRightCornerFactory.Verify(factory => factory.Create(), Times.Exactly(1));
            mockRoundCornerFactory.Verify(factory => factory.Create(), Times.Exactly(1));
        }
    }
}
