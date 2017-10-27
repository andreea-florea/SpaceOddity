using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;
using ViewModel.Actions;
using NaturalNumbersMath;

namespace ViewModel.Tests.Actions
{
    [TestClass]
    public class CreateBlockActionTest
    {
        [TestMethod]
        public void ActionCallsCreateBlockFromBlueprintBuilder()
        {
            var position = new Coordinate(4, 3);
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            var action = new CreateBlockAction(mockBlueprintBuilder.Object, position);
            action.Execute();
            mockBlueprintBuilder.Verify(builder => builder.CreateBlock(position), Times.Once());
        }
    }
}
