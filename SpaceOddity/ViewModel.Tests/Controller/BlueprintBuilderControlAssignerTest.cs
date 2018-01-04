using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewInterface;
using Game.Interfaces;
using ViewModel.Actions;
using NaturalNumbersMath;
using ViewModel.DataStructures;
using ViewModel.Controller;


namespace ViewModel.Tests.Controller
{
    [TestClass]
    public class BlueprintBuilderControlAssignerTest
    {
        [TestMethod]
        public void CheckIfLeftClickActionIsAssignedByControllerToTile()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var mockTile = new Mock<IWorldObject>();
            mockTile.SetupSet(tile => tile.LeftClickAction = It.IsAny<TileSelectAction>()).Verifiable();

            var position = new Coordinate(2, 3);
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignTileControl(mockTile.Object, position);

            mockTile.VerifySet(tile => tile.LeftClickAction = It.IsAny<TileSelectAction>());
        }

        [TestMethod]
        public void CheckIfRightClickActionIsAsssignedByControllerToBlock()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var mockBlock = new Mock<IWorldObject>();
            mockBlock.SetupSet(block => block.RightClickAction = It.IsAny<BlockSelectAction>()).Verifiable();

            var position = new Coordinate(4, 2);
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignBlockControl(mockBlock.Object, position);

            mockBlock.VerifySet(block => block.RightClickAction = It.IsAny<BlockCancelAction>());
        }

        [TestMethod]
        public void CheckIfLeftClickActionIsAsssignedByControllerToBlock()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var mockBlock = new Mock<IWorldObject>();
            mockBlock.SetupSet(block => block.LeftClickAction = It.IsAny<BlockSelectAction>()).Verifiable();

            var position = new Coordinate(4, 2);
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignBlockControl(mockBlock.Object, position);

            mockBlock.VerifySet(block => block.LeftClickAction = It.IsAny<BlockSelectAction>());
        }

        [TestMethod]
        public void CheckIfLeftClickActionIsAssignedByControllerToPipeLink()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var mockPipeLink = new Mock<IWorldObject>();
            mockPipeLink.SetupSet(pipeLink => pipeLink.LeftClickAction = It.IsAny<PipeLinkSelectAction>()).Verifiable();

            var edge = new CoordinatePair(new Coordinate(2, 2), new Coordinate(1, 2));
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignPipeLinkControl(mockPipeLink.Object, edge);

            mockPipeLink.VerifySet(pipeLink => pipeLink.LeftClickAction = It.IsAny<PipeLinkSelectAction>());
        }

        [TestMethod]
        public void CheckIfClickActionsAreAssignedByControllerToShipComponent()
        {
            var mockController = new Mock<IBlueprintBuilderController>();
            var mockShipComponent = new Mock<IWorldObject>();
            mockShipComponent.SetupSet(component => component.LeftClickAction = It.IsAny<ShipComponentSelectAction>()).Verifiable();
            mockShipComponent.SetupSet(component => component.RightClickAction = It.IsAny<ShipComponentCancelAction>()).Verifiable();

            var position = new Coordinate(1, 3);
            var blueprintBuilderControlAssigner = new BlueprintBuilderControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignShipComponentControl(mockShipComponent.Object, position);

            mockShipComponent.VerifySet(component => component.LeftClickAction = It.IsAny<ShipComponentSelectAction>());
            mockShipComponent.VerifySet(component => component.RightClickAction = It.IsAny<ShipComponentCancelAction>());
        }
    }
}
