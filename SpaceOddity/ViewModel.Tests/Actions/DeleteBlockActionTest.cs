using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Game.Interfaces;
using ViewModel.Actions;
using NaturalNumbersMath;

namespace ViewModel.Tests.Actions
{
    [TestClass]
    public class DeleteBlockActionTest
    {
        [TestMethod]
        public void ActionCallsDeleteBlockFromBlueprintBuilder()
        {
            var position = new Coordinate(4, 3);
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            var action = new DeleteBlockAction(mockBlueprintBuilder.Object, position);
            action.Execute();
            mockBlueprintBuilder.Verify(builder => builder.DeleteBlock(position), Times.Once());
        }
    }
}
