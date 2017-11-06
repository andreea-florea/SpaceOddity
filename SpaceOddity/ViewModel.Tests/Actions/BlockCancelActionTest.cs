using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using NaturalNumbersMath;
using ViewModel.Actions;

namespace ViewModel.Tests.Actions
{
    [TestClass]
    public class BlockCancelActionTest
    {
        [TestMethod]
        public void ActionCallsBlockCancel()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var position = new Coordinate(4, 5);
            var tileSelectAction = new BlockCancelAction(mockController.Object, position);

            tileSelectAction.Execute();

            mockController.Verify(controller => controller.BlockCancel(position), Times.Once());
        }
    }
}
