using System.Collections.Generic;
using Game.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NaturalNumbersMath;
using System.Linq;

namespace Game.Tests
{
    [TestClass]
    public class BlueprintBuilderTest
    {
        private IBlock[,] blueprint;
        private BlueprintBuilder blueprintBuilder;
        private Mock<IBlockFactory> mockBlockFactory;
        private Mock<IShipComponentFactory> mockShipComponentFactory;
        private Mock<IBlock> mockBlock;
        private Mock<IShipComponent> mockShipComponent;
        private List<DoubleEdgedPipe> doubleEdgedPipes;
        private List<ConnectingPipe> oneEdgedPipes;

        [TestInitialize]
        public void Init()
        {
            blueprint = new IBlock[9, 10];
            mockBlock = new Mock<IBlock>();
            mockShipComponent = new Mock<IShipComponent>();
            mockBlockFactory = new Mock<IBlockFactory>();
            mockShipComponentFactory = new Mock<IShipComponentFactory>();
            blueprintBuilder = new BlueprintBuilder(blueprint, mockBlockFactory.Object, mockShipComponentFactory.Object);
            doubleEdgedPipes = new List<DoubleEdgedPipe>();
            oneEdgedPipes = new List<ConnectingPipe>();

            mockBlock.SetupGet(x => x.PipesWithBothEdges).Returns(doubleEdgedPipes);
            mockBlock.SetupGet(x => x.PipesWithOneEdge).Returns(oneEdgedPipes);
        }

        [TestMethod]
        public void CheckThatHasBlockReturnsFalseWhenThereIsNoBlockAtSpecifiedPosition()
        {
            var position = new Coordinate(3, 2);
            Assert.IsFalse(blueprintBuilder.HasBlock(position));
        }

        [TestMethod]
        public void CheckThatHasBlockReturnsTrueWhenBlockExistsAtSpecifiedPosition()
        {
            var position = new Coordinate(3, 2);
            blueprint[2, 3] = mockBlock.Object;
            Assert.IsTrue(blueprintBuilder.HasBlock(position));
        }

        [TestMethod]
        public void HasBlockReturnsFalseIfPositionToCheckOutsideOfBlueprintBounds()
        {
            var position = new Coordinate(-3, 2);
            Assert.IsFalse(blueprintBuilder.HasBlock(position));
        }

        [TestMethod]
        public void CheckIfBlueprintMatrixIsAssignedCorrectly()
        {
            var position = new Coordinate(3, 2);
            blueprint[2, 3] = mockBlock.Object;
            Assert.AreEqual(9, blueprintBuilder.Dimensions.Y);
            Assert.AreEqual(10, blueprintBuilder.Dimensions.X);
            Assert.AreEqual(blueprint[2, 3], blueprintBuilder.GetBlock(position));
        }

        [TestMethod]
        public void CheckThatBlueprintMatrixDimensionsAreAssignedCorrectly()
        {
            var dimensions = new Coordinate(4, 5);
            var blueprintBuilder = new BlueprintBuilder(dimensions);
            Assert.AreEqual(4, blueprintBuilder.Dimensions.Y);
            Assert.AreEqual(5, blueprintBuilder.Dimensions.X);
        }

        [TestMethod]
        public void CheckThatBlockFactoryIsAssignedWhenUsingSimpleConstructor()
        {
            var dimensions = new Coordinate(4, 5);
            var blueprintBuilder = new BlueprintBuilder(dimensions);
            var position = new Coordinate(1, 2);
            Assert.IsTrue(blueprintBuilder.CreateBlock(position));
        }

        [TestMethod]
        public void CheckThatShipComponentFactoryIsAssignedWhenUsingSimpleConstructor()
        {
            var dimensions = new Coordinate(4, 5);
            var blueprintBuilder = new BlueprintBuilder(dimensions);
            var position = new Coordinate(1, 2);
            Assert.IsTrue(blueprintBuilder.CreateBlock(position));
            Assert.IsTrue(blueprintBuilder.AddShipComponent(position));
        }

        [TestMethod]
        public void CheckIfBlockGetsCreatedCorrectlyOnEmptySpot()
        {
            blueprint[4, 5] = mockBlock.Object;
            Assert.AreNotEqual(blueprint[4, 5], null);
        }

        [TestMethod]
        public void BlockCantBeCreatedIfSpotOccupied()
        {
            var position = new Coordinate(5, 4);
            blueprint[4, 5] = mockBlock.Object;
            Assert.IsFalse(blueprintBuilder.CreateBlock(position));
        }

        [TestMethod]
        public void CheckIfBlockFactoryIsUsedToCreateOtherBlocks()
        {
            var position = new Coordinate(5, 4);
            mockBlockFactory.Setup(x => x.CreateBlock()).Returns(mockBlock.Object);
            Assert.IsTrue(blueprintBuilder.CreateBlock(position));
            Assert.AreEqual(mockBlock.Object, blueprintBuilder.GetBlock(position));
        }

        [TestMethod]
        public void CheckIfShipComponentFactoryIsUsedToAddShipComponents()
        {
            var position = new Coordinate(5, 4);
            mockBlockFactory.Setup(x => x.CreateBlock()).Returns(mockBlock.Object);
            mockBlock.SetupAllProperties();
            mockShipComponentFactory.Setup(x => x.CreateComponent()).Returns(mockShipComponent.Object);
            Assert.IsTrue(blueprintBuilder.CreateBlock(position));
            Assert.IsTrue(blueprintBuilder.AddShipComponent(position));
            mockBlock.Verify(block => block.AddShipComponent(mockShipComponent.Object), Times.Once());
        }

        [TestMethod]
        public void CheckIfExistentBlockIsDeletedSuccessfully()
        {
            var position = new Coordinate(5, 4);
            blueprint[4, 5] = mockBlock.Object;
            Assert.AreNotEqual(null, blueprint[4, 5]);
            Assert.IsTrue(blueprintBuilder.DeleteBlock(position));
            Assert.AreEqual(null, blueprint[4, 5]);
        }

        [TestMethod]
        public void CheckThatInexistentBlockCannotBeDeleted()
        {
            var position = new Coordinate(5, 4);
            Assert.AreEqual(null, blueprint[4, 5]);
            Assert.IsFalse(blueprintBuilder.DeleteBlock(position));
        }

        [TestMethod]
        public void CheckIfShipComponentIsAddedCorrectlyOnBlockOnBlueprint()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns(mockShipComponent.Object);
            blueprint[4, 5] = mockBlock.Object;
            Assert.IsTrue(blueprintBuilder.AddShipComponent(position));
            Assert.AreEqual(mockShipComponent.Object, blueprint[4, 5].ShipComponent);
            mockBlock.Verify(x => x.AddShipComponent(It.IsAny<IShipComponent>()), Times.Once());
        }

        [TestMethod]
        public void CheckThatShipComponentCannotBeAddedOnInexistentBlock()
        {
            var position = new Coordinate(5, 4);
            Assert.IsFalse(blueprintBuilder.AddShipComponent(position));
            mockBlock.Verify(x => x.AddShipComponent(It.IsAny<IShipComponent>()), Times.Never());
        }

        [TestMethod]
        public void CheckIfShipComponentIsDeletedFromBlockOnBlueprint()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns(mockShipComponent.Object);
            mockBlock.Setup(m => m.HasShipComponent()).Returns(true);
            mockBlock.Setup(x => x.DeleteShipComponent()).Callback(() => mockBlock.SetupGet(m => m.ShipComponent).Returns((IShipComponent)null));
            blueprint.Set(position, mockBlock.Object);
            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            Assert.AreEqual(null, blueprint[4, 5].ShipComponent);
            mockBlock.Verify(x => x.DeleteShipComponent(), Times.Once());
        }

        [TestMethod]
        public void CheckThatShipComponentCannotBeDeletedIfInexistent()
        {
            var position = new Coordinate(5, 4);
            blueprint.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);
            Assert.IsFalse(blueprintBuilder.DeleteShipComponent(position));
            mockBlock.Verify(x => x.DeleteShipComponent(), Times.Never());
        }

        [TestMethod]
        public void CheckThatShipComponentCannotBeDeletedFromInexistentBlock()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns((IShipComponent)null);
            Assert.IsFalse(blueprintBuilder.DeleteShipComponent(position));
            mockBlock.Verify(x => x.DeleteShipComponent(), Times.Never());
        }

        [TestMethod]
        public void CheckThatShipComponentCannotBeAddedToBlockWithExistentShipComponent()
        {
            var position = new Coordinate(5, 4);
            blueprint.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(true);
            Assert.IsFalse(blueprintBuilder.AddShipComponent(position));
            mockBlock.Verify(x => x.AddShipComponent(mockShipComponent.Object), Times.Never());
        }

        [TestMethod]
        public void CanAddDoubleEdgedPipeIfNothingOnBlock()
        {
            var position = new Coordinate(5, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);

            blueprint.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe.FirstEdge, pipe.SecondEdge));
            Assert.AreEqual(1, mockBlock.Object.PipesWithBothEdges.Count);
            Assert.AreEqual(EdgeType.DOWN, doubleEdgedPipes[0].FirstEdge);
            Assert.AreEqual(EdgeType.UP, doubleEdgedPipes[0].SecondEdge);
        }

        [TestMethod]
        public void CannotAddDoubleEdgedPipeIfBlockInexistent()
        {
            var position = new Coordinate(5, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);

            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsFalse(blueprintBuilder.AddDoubleEdgedPipe(position, pipe.FirstEdge, pipe.SecondEdge));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count);
        }

        [TestMethod]
        public void CanAddDoubleEdgedPipeIfAnotherDoubleEdgedPipeAlreadyExistsAndDoesNotIntersect()
        {
            var position = new Coordinate(5, 4);
            var pipe1 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            var pipe2 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.RIGHT);

            blueprint.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe1.FirstEdge, pipe1.SecondEdge));
            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe2.FirstEdge, pipe2.SecondEdge));
            Assert.AreEqual(2, mockBlock.Object.PipesWithBothEdges.Count);
            Assert.AreEqual(EdgeType.DOWN, doubleEdgedPipes[0].FirstEdge);
            Assert.AreEqual(EdgeType.UP, doubleEdgedPipes[0].SecondEdge);
            Assert.AreEqual(EdgeType.DOWN, doubleEdgedPipes[1].FirstEdge);
            Assert.AreEqual(EdgeType.RIGHT, doubleEdgedPipes[1].SecondEdge);
        }

        [TestMethod]
        public void AfterTryingToAddDoubleEdgedPipeThatIntersectsWithExistingDoubleEdgedPipeNeitherWillBeFoundInDoubleEdgedPipesList()
        {
            var position = new Coordinate(5, 4);
            var pipe1 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            var pipe2 = new DoubleEdgedPipe(EdgeType.LEFT, EdgeType.RIGHT);

            blueprint.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe1.FirstEdge, pipe1.SecondEdge));
            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe2.FirstEdge, pipe2.SecondEdge));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count);
            Assert.AreEqual(4, mockBlock.Object.PipesWithOneEdge.Count);
            Assert.AreEqual(EdgeType.DOWN, mockBlock.Object.PipesWithOneEdge[0].Edge);
            mockBlock.Verify(x => x.AddShipComponent(It.IsAny<EmptyShipComponent>()), Times.Once());
        }

        [TestMethod]
        public void TryingToAddDoubleEdgedPipeOnBlockWithShipComponentResultsInAddingTwoConnectingPipes()
        {
            var position = new Coordinate(5, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            blueprint.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(true);

            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe.FirstEdge, pipe.SecondEdge));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count);
            Assert.AreEqual(2, mockBlock.Object.PipesWithOneEdge.Count);
            Assert.AreEqual(EdgeType.DOWN, mockBlock.Object.PipesWithOneEdge[0].Edge);
            Assert.AreEqual(EdgeType.UP, mockBlock.Object.PipesWithOneEdge[1].Edge);
        }

        [TestMethod]
        public void WhenAddingAShipComponentOnABlockWithPipesAllPipesTransformIntoConnectingPipes()
        {
            var position = new Coordinate(5, 4);
            blueprint.Set(position, mockBlock.Object);
            doubleEdgedPipes.Add(new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP));
            mockBlock.Setup(x => x.AddShipComponent(mockShipComponent.Object)).Callback(() => mockBlock.SetupGet(m => m.PipesWithBothEdges).Returns(new List<DoubleEdgedPipe>()));

            Assert.IsTrue(blueprintBuilder.AddShipComponent(position));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count);
            Assert.AreEqual(2, mockBlock.Object.PipesWithOneEdge.Count);
            Assert.AreEqual(EdgeType.DOWN, mockBlock.Object.PipesWithOneEdge[0].Edge);
            Assert.AreEqual(EdgeType.UP, mockBlock.Object.PipesWithOneEdge[1].Edge);
        }

        [TestMethod]
        public void WhenDeletingShipComponentFromBlockWithOneConnectingEdgeTheEdgeIsDeleted()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns(mockShipComponent.Object);
            mockBlock.Setup(m => m.HasShipComponent()).Returns(true);
            mockBlock.Setup(x => x.DeleteShipComponent()).Callback(() => mockBlock.SetupGet(m => m.ShipComponent).Returns((IShipComponent)null));
            blueprint.Set(position, mockBlock.Object);
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.UP));
            mockBlock.Setup(x => x.AddShipComponent(mockShipComponent.Object)).Callback(() => mockBlock.SetupGet(m => m.PipesWithOneEdge).Returns(new List<ConnectingPipe>()));

            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            Assert.AreEqual(null, blueprint[4, 5].ShipComponent);
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count);
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count);
            mockBlock.Verify(x => x.DeleteShipComponent(), Times.Once());
        }

        [TestMethod]
        public void WhenDeletingShipComponentFromBlockWithTwoConnectingEdgesTheEdgesAreDeletedAndTransformedToDoubleEdgePipe()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns(mockShipComponent.Object);
            mockBlock.Setup(m => m.HasShipComponent()).Returns(true);
            blueprint.Set(position, mockBlock.Object);
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.UP));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.DOWN));
            mockBlock.Setup(x => x.DeleteShipComponent()).Callback(() => mockBlock.SetupGet(m => m.PipesWithOneEdge).Returns(new List<ConnectingPipe>()));

            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count);
            Assert.AreEqual(1, mockBlock.Object.PipesWithBothEdges.Count);
            Assert.AreEqual(EdgeType.UP, doubleEdgedPipes[0].FirstEdge);
            Assert.AreEqual(EdgeType.DOWN, doubleEdgedPipes[0].SecondEdge);
        }

        [TestMethod]
        public void WhenDeletingShipComponentFromBlockWithThreeConnectingEdgesTheEdgesAreDeletedAndTransformedToDoubleEdgePipe()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns(mockShipComponent.Object);
            mockBlock.Setup(m => m.HasShipComponent()).Returns(true);
            blueprint.Set(position, mockBlock.Object);
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.UP));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.DOWN));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.RIGHT));
            mockBlock.Setup(x => x.DeleteShipComponent()).Callback(() => mockBlock.SetupGet(m => m.PipesWithOneEdge).Returns(new List<ConnectingPipe>()));

            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count);
            Assert.AreEqual(3, mockBlock.Object.PipesWithBothEdges.Count);
            Assert.AreEqual(EdgeType.UP, doubleEdgedPipes[0].FirstEdge);
            Assert.AreEqual(EdgeType.DOWN, doubleEdgedPipes[0].SecondEdge);
        }

        [TestMethod]
        public void WhenDeletingShipComponentFromBlockWithFourConnectingEdgesAnEmptyShipComponentIsAdded()
        {
            var empty = new EmptyShipComponent();
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns(mockShipComponent.Object);
            mockBlock.Setup(m => m.HasShipComponent()).Returns(true);
            blueprint.Set(position, mockBlock.Object);
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.UP));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.DOWN));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.LEFT));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.RIGHT));
            mockBlock.Setup(x => x.DeleteShipComponent()).Callback(() => mockBlock.SetupGet(m => m.ShipComponent).Returns(empty));

            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            Assert.AreEqual(4, mockBlock.Object.PipesWithOneEdge.Count);
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count);
            Assert.AreEqual(empty, mockBlock.Object.ShipComponent);
        }

        [TestMethod]
        public void CannotAddDoubleEdgedPipeThatAlreadyExistsToBlock()
        {
            var position = new Coordinate(5, 4);
            var pipe1 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            var pipe2 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);

            blueprint.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe1.FirstEdge, pipe1.SecondEdge));
            Assert.IsFalse(blueprintBuilder.AddDoubleEdgedPipe(position, pipe2.FirstEdge, pipe2.SecondEdge));
            Assert.AreEqual(1, mockBlock.Object.PipesWithBothEdges.Count);
            Assert.AreEqual(EdgeType.DOWN, doubleEdgedPipes[0].FirstEdge);
            Assert.AreEqual(EdgeType.UP, doubleEdgedPipes[0].SecondEdge);
        }

        [TestMethod]
        public void NoTwoConnectingPipesAreAddedToSameList()
        {
            var position = new Coordinate(5, 4);
            blueprint.Set(position, mockBlock.Object);
            doubleEdgedPipes.Add(new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP));
            doubleEdgedPipes.Add(new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.RIGHT));
            mockBlock.Setup(x => x.AddShipComponent(mockShipComponent.Object)).Callback(() => mockBlock.SetupGet(m => m.PipesWithBothEdges).Returns(new List<DoubleEdgedPipe>()));

            Assert.IsTrue(blueprintBuilder.AddShipComponent(position));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count);
            Assert.AreEqual(3, mockBlock.Object.PipesWithOneEdge.Count);
            Assert.AreEqual(EdgeType.DOWN, mockBlock.Object.PipesWithOneEdge[0].Edge);
            Assert.AreEqual(EdgeType.UP, mockBlock.Object.PipesWithOneEdge[1].Edge);
            Assert.AreEqual(EdgeType.RIGHT, mockBlock.Object.PipesWithOneEdge[2].Edge);
        }
    }
}
