using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewInterface;
using Game.Interfaces;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Controller;
using BlueprintBuildingViewModel.Fancy;
using ViewModel;

namespace BlueprintBuildingViewModel.Tests
{
    [TestClass]
    public class FancyViewModelFactoryTest
    {
        private Mock<IViewModelTilesFactory> mockTilesFactory;
        private IActivateableWorldObject[,] tiles;
        private Mock<IActivateableWorldObject> mockTile;
        private Mock<IRenderableFactory> mockBlockCoreFactory;
        private Mock<IRenderableFactory> mockRoundCornerFactory;
        private Mock<IRenderableFactory> mockStraightUpCornerFactory;
        private Mock<IRenderableFactory> mockStraightRightCornerFactory;
        private Mock<IRenderableFactory> mockClosedCornerFactory;
        private Mock<IRenderableFactory> mockOutsideUpCornerFactory;
        private Mock<IRenderableFactory> mockOutsideRightCornerFactory;
        private Mock<IRenderableFactory> mockDiagonalMissingCornerFactory;
        private Mock<IRenderableFactory> mockRoundEdgeFactory;
        private Mock<IRenderableFactory> mockClosedEdgeFactory;

        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IBlueprint> mockBlueprint;
        private Mock<IBlock> mockBlock;
        private Mock<IRenderable> mockDetail;
        private Mock<IRectangleSection> mockFittingRectangle;
        private FancyViewModelFactory blueprintBuilderViewModelFactory; 

        [TestInitialize]
        public void Initialize()
        {
            mockBlockCoreFactory = new Mock<IRenderableFactory>();
            mockRoundCornerFactory = new Mock<IRenderableFactory>();
            mockStraightUpCornerFactory = new Mock<IRenderableFactory>();
            mockStraightRightCornerFactory = new Mock<IRenderableFactory>();
            mockClosedCornerFactory = new Mock<IRenderableFactory>();
            mockOutsideUpCornerFactory = new Mock<IRenderableFactory>();
            mockOutsideRightCornerFactory = new Mock<IRenderableFactory>();
            mockDiagonalMissingCornerFactory = new Mock<IRenderableFactory>();
            mockRoundEdgeFactory = new Mock<IRenderableFactory>();
            mockClosedEdgeFactory = new Mock<IRenderableFactory>();

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
            mockRoundCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, position);

            mockRoundCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(4));
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
            mockRoundCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockStraightUpCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(2));
            mockRoundCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(2));
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
            mockRoundCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockOutsideUpCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockOutsideRightCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockDiagonalMissingCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockStraightUpCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(2));
            mockRoundCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(1));
            mockDiagonalMissingCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(1));
            mockOutsideUpCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(1));
            mockOutsideRightCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(1));
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
            mockRoundCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockRoundCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(5));
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
            mockRoundCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockClosedCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockClosedCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(4));
            mockStraightUpCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(2));
            mockRoundCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(1));
        }

        [TestMethod]
        public void RoundEdgesDetailsAreCreatedWithCorrectFactory()
        {
            tiles[1, 2] = mockTile.Object;
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockRoundEdgeFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(4));
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
            mockRoundCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockRoundEdgeFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(3));
            mockClosedEdgeFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(2));
        }

        [TestMethod]
        public void CenterBlockDetailIsCreatedCorrectly()
        {
            tiles.Set(new Coordinate(2, 1), mockTile.Object);
            mockBlueprintBuilder.Setup(builder => builder.Dimensions).Returns(new Coordinate(4, 3));
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 1))).Returns(true);
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(2, 1))).Returns(mockBlock.Object);
            mockRoundCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockBlockCoreFactory.Verify(factory => factory.CreateRenderable(), Times.Once());
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
            mockRoundCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockDiagonalMissingCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockStraightUpCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockStraightRightCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockOutsideRightCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockOutsideUpCornerFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockRoundEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockClosedEdgeFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);
            mockBlockCoreFactory.Setup(factory => factory.CreateRenderable()).Returns(mockDetail.Object);

            var viewModel = blueprintBuilderViewModelFactory.CreateViewModel(
                mockBlueprintBuilder.Object, mockFittingRectangle.Object);
            viewModel.BlockCreated(mockBlueprint.Object, new Coordinate(2, 1));

            mockDiagonalMissingCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(1));
            mockStraightUpCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(2));
            mockStraightRightCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(2));
            mockOutsideUpCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(1));
            mockOutsideRightCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(1));
            mockRoundCornerFactory.Verify(factory => factory.CreateRenderable(), Times.Exactly(1));
        }
    }
}
