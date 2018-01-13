using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Actions;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Tests.Actions
{
    [TestClass]
    public class BlockSelectActionTest
    {
        [TestMethod]
        public void ActionCallsBlockSelect()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var position = new Coordinate(4, 5);
            var blockSelectAction = new BlockSelectAction(mockController.Object, position);

            blockSelectAction.Execute();

            mockController.Verify(controller => controller.BlockSelect(position), Times.Once());
        }
    }
}
