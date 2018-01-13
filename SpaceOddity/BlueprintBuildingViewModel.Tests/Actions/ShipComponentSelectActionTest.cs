using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.Controller;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Actions;

namespace BlueprintBuildingViewModel.Tests.Actions
{
    [TestClass]
    public class ShipComponentSelectActionTest
    {
        [TestMethod]
        public void ActionCallsShipComponentSelect()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var position = new Coordinate(3, 5);
            var componentSelectAction = new ShipComponentSelectAction(mockController.Object, position);

            componentSelectAction.Execute();

            mockController.Verify(controller => controller.ShipComponentSelect(position), Times.Once());
        }
    }
}
