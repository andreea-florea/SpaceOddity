using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Controller;
using Moq;
using NaturalNumbersMath;
using ViewModel.Actions;

namespace ViewModel.Tests.Actions
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
