using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Actions;
using Moq;
using ViewModel.Interfaces;
using NaturalNumbersMath;
using Game.Interfaces;

namespace ViewModel.Tests.Actions
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
