using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.Actions;
using NaturalNumbersMath;
using Moq;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Tests.Actions
{
    [TestClass]
    public class ShipComponentCancelTest
    {
        [TestMethod]
        public void ActionCallsShipComponentSelect()
        {
            var mockController = new Mock<IController>();
            var position = new Coordinate(3, 5);
            var componentSelectAction = new ShipComponentCancel(mockController.Object, position);

            componentSelectAction.Execute();

            mockController.Verify(controller => controller.CancelShipComponent(position), Times.Once());
        }
    }
}
