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
            var tile = new Mock<IWorldObject>();
            tile.SetupAllProperties();

            var position = new Coordinate(2, 3);
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignTileControl(tile.Object, position);

            tile.Object.LeftClickAction.Execute();
            mockController.Verify(controller => controller.TileSelect(position));
        }

        [TestMethod]
        public void CheckIfRightClickActionIsAsssignedByControllerToBlock()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var block = new Mock<IWorldObject>();
            block.SetupAllProperties();

            var position = new Coordinate(4, 2);
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignBlockControl(block.Object, position);

            block.Object.RightClickAction.Execute();
            mockController.Verify(controller => controller.BlockCancel(position));
        }

        [TestMethod]
        public void CheckIfLeftClickActionIsAsssignedByControllerToBlock()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var block = new Mock<IWorldObject>();
            block.SetupAllProperties();

            var position = new Coordinate(4, 2);
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignBlockControl(block.Object, position);

            block.Object.LeftClickAction.Execute();
            mockController.Verify(controller => controller.BlockSelect(position));
        }
    }
}
