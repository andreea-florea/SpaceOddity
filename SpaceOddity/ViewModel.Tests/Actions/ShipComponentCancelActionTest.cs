using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Actions;
using NaturalNumbersMath;
using Moq;
using ViewModel.Controller;

namespace ViewModel.Tests.Actions
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
