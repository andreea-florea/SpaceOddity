using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Actions;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Tests.Actions
{
    [TestClass]
    public class BlockCancelTest
    {
        [TestMethod]
        public void ActionCallsBlockCancel()
        {
            var mockController = new Mock<IController>();
            var position = new Coordinate(4, 5);
            var tileSelectAction = new BlockCancel(mockController.Object, position);

            tileSelectAction.Execute();

            mockController.Verify(controller => controller.CancelBlock(position), Times.Once());
        }
    }
}
