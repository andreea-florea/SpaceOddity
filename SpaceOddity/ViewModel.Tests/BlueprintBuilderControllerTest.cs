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
    public class BlueprintBuilderControllerTest
    {
        [TestMethod]
        public void CheckIfLeftClickActionIsAssignedByControllerToTile()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            var tile = new Mock<IWorldObject>();
            tile.SetupAllProperties();

            var position = new Coordinate(2, 3);
            var blueprintBuilderController = new BlueprintBuilderController();
            blueprintBuilderController.AssignTileControl(mockBlueprintBuilder.Object, tile.Object, position);

            tile.Object.LeftClickAction.Execute();
            mockBlueprintBuilder.Verify(builder => builder.CreateBlock(position));
        }

        [TestMethod]
        public void CheckIfRightClickActionIsAsssignedByControllerToBlock()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            var block = new Mock<IWorldObject>();
            block.SetupAllProperties();

            var position = new Coordinate(4, 2);
            var blueprintBuilderController = new BlueprintBuilderController();
            blueprintBuilderController.AssignBlockControl(mockBlueprintBuilder.Object, block.Object, position);

            block.Object.RightClickAction.Execute();
            mockBlueprintBuilder.Verify(builder => builder.DeleteBlock(position));
        }
    }
}
