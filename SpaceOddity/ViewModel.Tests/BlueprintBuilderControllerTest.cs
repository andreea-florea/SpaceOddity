using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewInterface;
using Game.Interfaces;

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

            var blueprintBuilderController = new BlueprintBuilderController();
            blueprintBuilderController.AssignTileControl(mockBlueprintBuilder.Object, tile.Object, 2, 3);
            Assert.IsTrue(tile.Object.LeftClickAction is CreateBlockAction);
        }
    }
}
