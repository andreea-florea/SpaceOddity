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
        public void CheckIfLeftClickActionIsAssignedByController()
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
    }
}
