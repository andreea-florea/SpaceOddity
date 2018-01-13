using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.Actions;
using NaturalNumbersMath;
using Moq;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Tests.Actions
{
    [TestClass]
    public class ShipComponentCancelActionTest
    {
        [TestMethod]
        public void ActionCallsShipComponentSelect()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var position = new Coordinate(3, 5);
            var componentSelectAction = new ShipComponentCancelAction(mockController.Object, position);

            componentSelectAction.Execute();

            mockController.Verify(controller => controller.ShipComponentCancel(position), Times.Once());
        }
    }
}
