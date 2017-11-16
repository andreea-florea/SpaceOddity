using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewInterface;
using Game.Interfaces;
using ViewModel.Actions;
using NaturalNumbersMath;


namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderControlAssignerTest
    {
        [TestMethod]
        public void CheckIfLeftClickActionIsAssignedByControllerToTile()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var mockTile = new Mock<IWorldObject>();
            mockTile.SetupSet(tile => tile.LeftClickAction = It.IsAny<TileSelectAction>()).Verifiable();

            var position = new Coordinate(2, 3);
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignTileControl(mockTile.Object, position);

            mockTile.VerifySet(tile => tile.LeftClickAction = It.IsAny<TileSelectAction>());
        }

        [TestMethod]
        public void CheckIfRightClickActionIsAsssignedByControllerToBlock()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var mockBlock = new Mock<IWorldObject>();
            mockBlock.SetupSet(block => block.RightClickAction = It.IsAny<BlockSelectAction>()).Verifiable();

            var position = new Coordinate(4, 2);
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignBlockControl(mockBlock.Object, position);

            mockBlock.VerifySet(block => block.RightClickAction = It.IsAny<BlockCancelAction>());
        }

        [TestMethod]
        public void CheckIfLeftClickActionIsAsssignedByControllerToBlock()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var mockBlock = new Mock<IWorldObject>();
            mockBlock.SetupSet(block => block.LeftClickAction = It.IsAny<BlockSelectAction>()).Verifiable();

            var position = new Coordinate(4, 2);
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignBlockControl(mockBlock.Object, position);

            mockBlock.VerifySet(block => block.LeftClickAction = It.IsAny<BlockSelectAction>());
        }
    }
}
