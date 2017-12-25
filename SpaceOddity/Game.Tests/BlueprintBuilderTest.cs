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
        private IBlock[,] blocks;
        private Blueprint blueprint;
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
            blocks = new IBlock[9, 10];
            blueprint = new Blueprint(blocks);
            mockBlock = new Mock<IBlock>();
            mockShipComponent = new Mock<IShipComponent>();
            mockBlockFactory = new Mock<IBlockFactory>();
            mockShipComponentFactory = new Mock<IShipComponentFactory>();
            blueprintBuilder = new BlueprintBuilder(blueprint, mockBlockFactory.Object, mockShipComponentFactory.Object);
            doubleEdgedPipes = new List<DoubleEdgedPipe>();
            oneEdgedPipes = new List<ConnectingPipe>();

            mockBlock.SetupGet(x => x.PipesWithBothEdges).Returns(doubleEdgedPipes);
            mockBlock.SetupGet(x => x.PipesWithOneEdge).Returns(oneEdgedPipes);
            mockBlock.Setup(block => block.AddPipe(It.IsAny<ConnectingPipe>())).Callback<ConnectingPipe>(p => oneEdgedPipes.Add(p));
            mockBlock.Setup(block => block.AddPipe(It.IsAny<DoubleEdgedPipe>())).Callback<DoubleEdgedPipe>(p => doubleEdgedPipes.Add(p));
            mockBlock.Setup(block => block.DeletePipe(It.IsAny<ConnectingPipe>())).Callback<ConnectingPipe>(p => oneEdgedPipes.Remove(p));
            mockBlock.Setup(block => block.DeletePipe(It.IsAny<DoubleEdgedPipe>())).Callback<DoubleEdgedPipe>(p => doubleEdgedPipes.Remove(p));
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
            blocks[2, 3] = mockBlock.Object;
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
            blocks[2, 3] = mockBlock.Object;
            Assert.AreEqual(9, blueprintBuilder.Dimensions.Y);
            Assert.AreEqual(10, blueprintBuilder.Dimensions.X);
            Assert.AreEqual(blocks[2, 3], blueprintBuilder.GetBlock(position));
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
            blocks[4, 5] = mockBlock.Object;
            Assert.AreNotEqual(blocks[4, 5], null);
        }

        [TestMethod]
        public void BlockCantBeCreatedIfSpotOccupied()
        {
            var position = new Coordinate(5, 4);
            blocks[4, 5] = mockBlock.Object;
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
            mockShipComponentFactory.Setup(x => x.CreateComponent()).Returns(mockShipComponent.Object);
            Assert.IsTrue(blueprintBuilder.CreateBlock(position));
            Assert.IsTrue(blueprintBuilder.AddShipComponent(position));
            mockBlock.Verify(block => block.AddShipComponent(mockShipComponent.Object), Times.Once());
        }

        [TestMethod]
        public void CheckIfExistentBlockIsDeletedSuccessfully()
        {
            var position = new Coordinate(5, 4);
            blocks[4, 5] = mockBlock.Object;
            Assert.AreNotEqual(null, blocks[4, 5]);
            Assert.IsTrue(blueprintBuilder.DeleteBlock(position));
            Assert.AreEqual(null, blocks[4, 5]);
        }

        [TestMethod]
        public void CheckThatInexistentBlockCannotBeDeleted()
        {
            var position = new Coordinate(5, 4);
            Assert.AreEqual(null, blocks[4, 5]);
            Assert.IsFalse(blueprintBuilder.DeleteBlock(position));
        }

        [TestMethod]
        public void CheckIfShipComponentIsAddedCorrectlyOnBlockOnBlueprint()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupGet(m => m.ShipComponent).Returns(mockShipComponent.Object);
            blocks[4, 5] = mockBlock.Object;
            Assert.IsTrue(blueprintBuilder.AddShipComponent(position));
            Assert.AreEqual(mockShipComponent.Object, blocks[4, 5].ShipComponent);
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
            blocks.Set(position, mockBlock.Object);
            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            mockBlock.Verify(x => x.DeleteShipComponent(), Times.Once());
        }

        [TestMethod]
        public void CheckThatShipComponentCannotBeDeletedIfInexistent()
        {
            var position = new Coordinate(5, 4);
            blocks.Set(position, mockBlock.Object);
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
            blocks.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(true);
            Assert.IsFalse(blueprintBuilder.AddShipComponent(position));
            mockBlock.Verify(x => x.AddShipComponent(mockShipComponent.Object), Times.Never());
        }

        [TestMethod]
        public void CanAddDoubleEdgedPipeIfNothingOnBlock()
        {
            var position = new Coordinate(5, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);

            blocks.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe.FirstEdge, pipe.SecondEdge));
            Assert.AreEqual(1, mockBlock.Object.PipesWithBothEdges.Count());
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
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count());
        }

        [TestMethod]
        public void CanAddDoubleEdgedPipeIfAnotherDoubleEdgedPipeAlreadyExistsAndDoesNotIntersect()
        {
            var position = new Coordinate(5, 4);
            var pipe1 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            var pipe2 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.RIGHT);

            blocks.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            doubleEdgedPipes.Add(pipe1);
            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe2.FirstEdge, pipe2.SecondEdge));
            Assert.AreEqual(2, mockBlock.Object.PipesWithBothEdges.Count());
            Assert.AreEqual(EdgeType.DOWN, doubleEdgedPipes[1].FirstEdge);
            Assert.AreEqual(EdgeType.RIGHT, doubleEdgedPipes[1].SecondEdge);
        }
        
        [TestMethod]
        public void AfterTryingToAddDoubleEdgedPipeThatIntersectsWithExistingDoubleEdgedPipeNeitherWillBeFoundInDoubleEdgedPipesList()
        {
            var position = new Coordinate(5, 4);
            var pipe1 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            var pipe2 = new DoubleEdgedPipe(EdgeType.LEFT, EdgeType.RIGHT);

            blocks.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);
            mockBlock.Setup(block => block.AddShipComponent(It.IsAny<EmptyShipComponent>())).Callback(() => mockBlock.Setup(x => x.HasShipComponent()).Returns(true));
            
            doubleEdgedPipes.Add(pipe1);
            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe2.FirstEdge, pipe2.SecondEdge));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count());
            Assert.AreEqual(4, mockBlock.Object.PipesWithOneEdge.Count());
            Assert.AreEqual(EdgeType.DOWN, mockBlock.Object.PipesWithOneEdge.First().Edge);
            mockBlock.Verify(x => x.AddShipComponent(It.IsAny<EmptyShipComponent>()), Times.Once());
        }

        [TestMethod]
        public void TryingToAddDoubleEdgedPipeOnBlockWithShipComponentResultsInAddingTwoConnectingPipes()
        {
            var position = new Coordinate(5, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            blocks.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(true);

            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe.FirstEdge, pipe.SecondEdge));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count());
            Assert.AreEqual(2, mockBlock.Object.PipesWithOneEdge.Count());
            Assert.AreEqual(EdgeType.DOWN, mockBlock.Object.PipesWithOneEdge.First().Edge);
            Assert.AreEqual(EdgeType.UP, mockBlock.Object.PipesWithOneEdge.Skip(1).First().Edge);
        }

        [TestMethod]
        public void CannotAddConnectingPipeIfNoShipComponentOnBlock()
        {
            var position = new Coordinate(5, 4);
            var pipe = new ConnectingPipe(EdgeType.DOWN);

            blocks.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsFalse(blueprintBuilder.AddConnectingPipe(position, pipe.Edge));
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count());
        }

        [TestMethod]
        public void CannotAddConnectingPipeIfBlockInexistent()
        {
            var position = new Coordinate(5, 4);
            var pipe = new ConnectingPipe(EdgeType.DOWN);

            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsFalse(blueprintBuilder.AddConnectingPipe(position, pipe.Edge));
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count());
        }

        [TestMethod]
        public void CheckThatConnectingPipeCannotBeDeletedFromInexistentBlock()
        {
            var position = new Coordinate(5, 4);
            var pipe = new ConnectingPipe(EdgeType.DOWN);

            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsFalse(blueprintBuilder.DeleteConnectingPipe(position, pipe));
        }

        [TestMethod]
        public void CheckThatConnectingPipeCannotBeDeletedIfInexistent()
        {
            var position = new Coordinate(5, 4);
            var pipe = new ConnectingPipe(EdgeType.DOWN);

            blocks.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsFalse(blueprintBuilder.DeleteConnectingPipe(position, pipe));
        }

        [TestMethod]
        public void CheckThatIfAllConnectingPipesOnBlockAreDeletedShipComponentIsNot()
        {
            var position = new Coordinate(5, 4);
            var pipe = new ConnectingPipe(EdgeType.DOWN);

            blocks.Set(position, mockBlock.Object);
            blocks.Get(position).AddPipe(pipe);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(true);

            Assert.IsTrue(blueprintBuilder.DeleteConnectingPipe(position, pipe));
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count());
            mockBlock.Verify(x => x.DeleteShipComponent(), Times.Never());
        }

        [TestMethod]
        public void CheckIfExistingConnectingPipeIsDeletedSuccessfully()
        {
            var position = new Coordinate(5, 4);
            var pipe = new ConnectingPipe(EdgeType.DOWN);

            blocks.Set(position, mockBlock.Object);
            blocks.Get(position).AddPipe(pipe);            
            mockBlock.Setup(x => x.HasShipComponent()).Returns(true);

            Assert.IsTrue(blueprintBuilder.DeleteConnectingPipe(position, pipe));
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count());
        }

        [TestMethod]
        public void CheckThatDoubleEdgedPipeCannotBeDeletedFromInexistentBlock()
        {
            var position = new Coordinate(5, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);

            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsFalse(blueprintBuilder.DeleteDoubleEdgedPipe(position, pipe));
        }

        [TestMethod]
        public void CheckThatTryingToDeleteInexistentDoubleEdgedPipeButWithConnectingPipesThatCanComposeItDeletesTheConnectingPipes()
        {
            var position = new Coordinate(5, 4);
            var pipe1 = new ConnectingPipe(EdgeType.DOWN);
            var pipe2 = new ConnectingPipe(EdgeType.UP);
            var pipe = new DoubleEdgedPipe(EdgeType.UP, EdgeType.DOWN);

            blocks.Set(position, mockBlock.Object);
            blocks.Get(position).AddPipe(pipe1);
            blocks.Get(position).AddPipe(pipe2);
            
            Assert.IsTrue(blueprintBuilder.DeleteDoubleEdgedPipe(position, pipe));
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count());
        }

        [TestMethod]
        public void CheckThatInexistentDoubleEdgedPipeAndWithNoConnectingPipesThatCanComposeItCannotBeDeleted()
        {
            var position = new Coordinate(5, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.UP, EdgeType.DOWN);

            blocks.Set(position, mockBlock.Object);

            Assert.IsFalse(blueprintBuilder.DeleteDoubleEdgedPipe(position, pipe));
        }

        [TestMethod]
        public void CheckIfExistingDoubleEdgedPipeIsDeletedSuccessfully()
        {
            var position = new Coordinate(5, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);

            blocks.Set(position, mockBlock.Object);
            blocks.Get(position).AddPipe(pipe);

            Assert.IsTrue(blueprintBuilder.DeleteDoubleEdgedPipe(position, pipe));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count());
        }

        [TestMethod]
        public void CheckThatDoubleEdgedPipeIsDeletedSuccessfullyEvenIfOrderOfEdgesDiffers()
        {
            var position = new Coordinate(5, 4);
            var pipe = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            var pipe1 = new DoubleEdgedPipe(EdgeType.UP, EdgeType.DOWN);

            blocks.Set(position, mockBlock.Object);
            blocks.Get(position).AddPipe(pipe);

            Assert.IsTrue(blueprintBuilder.DeleteDoubleEdgedPipe(position, pipe1));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count());
        }

        [TestMethod]
        public void WhenAddingAShipComponentOnABlockWithPipesAllPipesTransformIntoConnectingPipes()
        {
            var position = new Coordinate(5, 4);
            blocks.Set(position, mockBlock.Object);
            doubleEdgedPipes.Add(new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP));

            mockBlock.Setup(block => block.AddShipComponent(It.IsAny<EmptyShipComponent>())).Callback(() => mockBlock.Setup(x => x.HasShipComponent()).Returns(true));

            Assert.IsTrue(blueprintBuilder.AddShipComponent(position));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count());
            Assert.AreEqual(2, mockBlock.Object.PipesWithOneEdge.Count());
            Assert.AreEqual(EdgeType.DOWN, mockBlock.Object.PipesWithOneEdge.First().Edge);
            Assert.AreEqual(EdgeType.UP, mockBlock.Object.PipesWithOneEdge.Skip(1).First().Edge);
        }

        [TestMethod]
        public void WhenDeletingShipComponentFromBlockWithOneConnectingEdgeTheEdgeIsDeleted()
        {
            var position = new Coordinate(5, 4);
            mockBlock.Setup(m => m.HasShipComponent()).Returns(true);
            blocks.Set(position, mockBlock.Object);
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.UP));
            
            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count());
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count());
        }

        [TestMethod]
        public void WhenDeletingShipComponentFromBlockWithTwoConnectingEdgesTheEdgesAreDeletedAndTransformedToDoubleEdgePipe()
        {
            var position = new Coordinate(5, 4);
            mockBlock.Setup(m => m.HasShipComponent()).Returns(true);
            mockBlock.Setup(x => x.DeleteShipComponent()).Callback(() => mockBlock.Setup(m => m.HasShipComponent()).Returns(false));
            blocks.Set(position, mockBlock.Object);
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.UP));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.DOWN));
            
            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count());
            Assert.AreEqual(1, mockBlock.Object.PipesWithBothEdges.Count());
            Assert.AreEqual(EdgeType.UP, doubleEdgedPipes[0].FirstEdge);
            Assert.AreEqual(EdgeType.DOWN, doubleEdgedPipes[0].SecondEdge);
        }

        [TestMethod]
        public void WhenDeletingShipComponentFromBlockWithThreeConnectingEdgesTheEdgesAreDeletedAndTransformedToDoubleEdgePipe()
        {
            var position = new Coordinate(5, 4);
            mockBlock.Setup(m => m.HasShipComponent()).Returns(true);
            mockBlock.Setup(x => x.DeleteShipComponent()).Callback(() => mockBlock.Setup(m => m.HasShipComponent()).Returns(false));
            blocks.Set(position, mockBlock.Object);
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.UP));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.DOWN));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.RIGHT));
            
            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            Assert.AreEqual(0, mockBlock.Object.PipesWithOneEdge.Count());
            Assert.AreEqual(3, mockBlock.Object.PipesWithBothEdges.Count());
            Assert.AreEqual(EdgeType.UP, doubleEdgedPipes[0].FirstEdge);
            Assert.AreEqual(EdgeType.DOWN, doubleEdgedPipes[0].SecondEdge);
        }

        [TestMethod]
        public void WhenDeletingShipComponentFromBlockWithFourConnectingEdgesAnEmptyShipComponentIsAdded()
        {
            var position = new Coordinate(5, 4);
            mockBlock.SetupSequence(m => m.HasShipComponent()).Returns(true).Returns(false);
            blocks.Set(position, mockBlock.Object);
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.UP));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.DOWN));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.LEFT));
            oneEdgedPipes.Add(new ConnectingPipe(EdgeType.RIGHT));

            var callOrder = 0;
            mockBlock.Setup(x => x.DeleteShipComponent()).Callback(() => Assert.AreEqual(0, callOrder++));
            mockBlock.Setup(x => x.AddShipComponent(It.IsAny<EmptyShipComponent>())).Callback(() => Assert.AreEqual(1, callOrder++));
            mockBlock.Setup(block => block.AddShipComponent(It.IsAny<EmptyShipComponent>())).Callback(() => mockBlock.Setup(x => x.HasShipComponent()).Returns(true));

            Assert.IsTrue(blueprintBuilder.DeleteShipComponent(position));
            Assert.AreEqual(4, mockBlock.Object.PipesWithOneEdge.Count());
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count());
            mockBlock.Verify(x => x.DeleteShipComponent(), Times.Once());
            mockBlock.Verify(x => x.AddShipComponent(It.IsAny<EmptyShipComponent>()), Times.Once());
        }

        [TestMethod]
        public void CannotAddDoubleEdgedPipeThatAlreadyExistsToBlock()
        {
            var position = new Coordinate(5, 4);
            var pipe1 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);
            var pipe2 = new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP);

            blocks.Set(position, mockBlock.Object);
            mockBlock.Setup(x => x.HasShipComponent()).Returns(false);

            Assert.IsTrue(blueprintBuilder.AddDoubleEdgedPipe(position, pipe1.FirstEdge, pipe1.SecondEdge));
            Assert.IsFalse(blueprintBuilder.AddDoubleEdgedPipe(position, pipe2.FirstEdge, pipe2.SecondEdge));
            Assert.AreEqual(1, mockBlock.Object.PipesWithBothEdges.Count());
            Assert.AreEqual(EdgeType.DOWN, doubleEdgedPipes[0].FirstEdge);
            Assert.AreEqual(EdgeType.UP, doubleEdgedPipes[0].SecondEdge);
        }

        [TestMethod]
        public void NoTwoConnectingPipesAreAddedToSameList()
        {
            var position = new Coordinate(5, 4);
            blocks.Set(position, mockBlock.Object);
            doubleEdgedPipes.Add(new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.UP));
            doubleEdgedPipes.Add(new DoubleEdgedPipe(EdgeType.DOWN, EdgeType.RIGHT));

            mockBlock.Setup(block => block.AddShipComponent(It.IsAny<EmptyShipComponent>())).Callback(() => mockBlock.Setup(x => x.HasShipComponent()).Returns(true));

            Assert.IsTrue(blueprintBuilder.AddShipComponent(position));
            Assert.AreEqual(0, mockBlock.Object.PipesWithBothEdges.Count());
            Assert.AreEqual(3, mockBlock.Object.PipesWithOneEdge.Count());
            Assert.AreEqual(EdgeType.DOWN, mockBlock.Object.PipesWithOneEdge.First().Edge);
            Assert.AreEqual(EdgeType.UP, mockBlock.Object.PipesWithOneEdge.Skip(1).First().Edge);
            Assert.AreEqual(EdgeType.RIGHT, mockBlock.Object.PipesWithOneEdge.Skip(2).First().Edge);
        }

        [TestMethod]
        public void CanAttachObserverToBuilderThroughBlueprintBuilder()
        {
            var mockObserver = new Mock<IBlueprintObserver>();
            blueprintBuilder.AttachObserver(mockObserver.Object);
            blueprint.PlaceBlock(new Coordinate(2, 3), mockBlock.Object);
            mockObserver.Verify(observer => observer.BlockCreated(blueprint, new Coordinate(2, 3)));
        }
    }
}
