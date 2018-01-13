using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.Actions;
using Moq;
using NaturalNumbersMath;
using Game.Interfaces;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Tests.Actions
{
    [TestClass]
    public class TileSelectActionTest
    {
        [TestMethod]
        public void ActionCallsTileSelect()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var position = new Coordinate(3, 5);
            var tileSelectAction = new TileSelectAction(mockController.Object, position);

            tileSelectAction.Execute();

            mockController.Verify(controller => controller.TileSelect(position), Times.Once());
        }
    }
}
