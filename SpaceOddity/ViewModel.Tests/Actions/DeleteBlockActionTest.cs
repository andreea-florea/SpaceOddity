using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Game.Interfaces;
using ViewModel.Actions;

namespace ViewModel.Tests.Actions
{
    [TestClass]
    public class DeleteBlockActionTest
    {
        [TestMethod]
        public void ActionCallsDeleteBlockFromBlueprintBuilder()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            var action = new DeleteBlockAction(mockBlueprintBuilder.Object, 4, 3);
            action.Execute();
            mockBlueprintBuilder.Verify(builder => builder.DeleteBlock(3, 4), Times.Once());
        }
    }
}
