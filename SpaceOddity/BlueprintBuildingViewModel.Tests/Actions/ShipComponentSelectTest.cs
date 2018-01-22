using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.Controller;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Actions;

namespace BlueprintBuildingViewModel.Tests.Actions
{
    [TestClass]
    public class ShipComponentSelectTest
    {
        [TestMethod]
        public void ActionCallsShipComponentSelect()
        {
            var mockController = new Mock<IController>();
            var position = new Coordinate(3, 5);
            var componentSelectAction = new ShipComponentSelect(mockController.Object, position);

            componentSelectAction.Execute();

            mockController.Verify(controller => controller.SelectShipComponent(position), Times.Once());
        }
    }
}
