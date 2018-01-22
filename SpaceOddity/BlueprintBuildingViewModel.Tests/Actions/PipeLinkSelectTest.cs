using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Actions;
using BlueprintBuildingViewModel.DataStructures;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Tests.Actions
{
    [TestClass]
    public class PipeLinkSelectTest
    {
        [TestMethod]
        public void ActionCallsPipeLinkSelect()
        {
            var mockController = new Mock<IController>();
            var edge = new CoordinatePair(new Coordinate(4, 5), new Coordinate(3, 5));
            var pipeLinkSelectAction = new PipeLinkSelect(mockController.Object, edge);

            pipeLinkSelectAction.Execute();

            mockController.Verify(controller => controller.SelectPipeLink(edge), Times.Once());
        }
    }
}
