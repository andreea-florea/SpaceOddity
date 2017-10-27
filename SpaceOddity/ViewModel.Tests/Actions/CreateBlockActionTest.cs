using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;
using ViewModel.Actions;

namespace ViewModel.Tests.Actions
{
    [TestClass]
    public class CreateBlockActionTest
    {
        [TestMethod]
        public void ActionCallsCreateBlockFromBlueprintBuilder()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            var action = new CreateBlockAction(mockBlueprintBuilder.Object, 4, 3);
            action.Execute();
            mockBlueprintBuilder.Verify(builder => builder.CreateBlock(3, 4), Times.Once());
        }
    }
}
