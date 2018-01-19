using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Actions;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Tests.Actions
{
    [TestClass]
    public class BlockSelectTest
    {
        [TestMethod]
        public void ActionCallsBlockSelect()
        {
            var mockController = new Mock<IController>();
            var position = new Coordinate(4, 5);
            var blockSelectAction = new BlockSelect(mockController.Object, position);

            blockSelectAction.Execute();

            mockController.Verify(controller => controller.SelectBlock(position), Times.Once());
        }
    }
}
