using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewInterface;
using Game.Interfaces;
using BlueprintBuildingViewModel.Actions;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.DataStructures;
using BlueprintBuildingViewModel.Controller;
using ViewModel;


namespace BlueprintBuildingViewModel.Tests.Controller
{
    [TestClass]
    public class ControlAssignerTest
    {
        [TestMethod]
        public void CheckIfLeftClickActionIsAssignedByControllerToTile()
        {
            var mockController = new Mock<IController>();
            var mockTile = new Mock<IWorldObject>();
            mockTile.SetupSet(tile => tile.LeftClickAction = It.IsAny<TileSelect>()).Verifiable();

            var position = new Coordinate(2, 3);
            var blueprintBuilderControlAssigner = new ControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignTileControl(mockTile.Object, position);

            mockTile.VerifySet(tile => tile.LeftClickAction = It.IsAny<TileSelect>());
        }

        [TestMethod]
        public void CheckIfRightClickActionIsAsssignedByControllerToBlock()
        {
            var mockController = new Mock<IController>();
            var mockBlock = new Mock<IWorldObject>();
            mockBlock.SetupSet(block => block.RightClickAction = It.IsAny<BlockSelect>()).Verifiable();

            var position = new Coordinate(4, 2);
            var blueprintBuilderControlAssigner = new ControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignBlockControl(mockBlock.Object, position);

            mockBlock.VerifySet(block => block.RightClickAction = It.IsAny<BlockCancel>());
        }

        [TestMethod]
        public void CheckIfLeftClickActionIsAsssignedByControllerToBlock()
        {
            var mockController = new Mock<IController>();
            var mockBlock = new Mock<IWorldObject>();
            mockBlock.SetupSet(block => block.LeftClickAction = It.IsAny<BlockSelect>()).Verifiable();

            var position = new Coordinate(4, 2);
            var blueprintBuilderControlAssigner = new ControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignBlockControl(mockBlock.Object, position);

            mockBlock.VerifySet(block => block.LeftClickAction = It.IsAny<BlockSelect>());
        }

        [TestMethod]
        public void CheckIfLeftClickActionIsAssignedByControllerToPipeLink()
        {
            var mockController = new Mock<IController>();
            var mockPipeLink = new Mock<IWorldObject>();
            mockPipeLink.SetupSet(pipeLink => pipeLink.LeftClickAction = It.IsAny<PipeLinkSelect>()).Verifiable();

            var edge = new CoordinatePair(new Coordinate(2, 2), new Coordinate(1, 2));
            var blueprintBuilderControlAssigner = new ControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignPipeLinkControl(mockPipeLink.Object, edge);

            mockPipeLink.VerifySet(pipeLink => pipeLink.LeftClickAction = It.IsAny<PipeLinkSelect>());
        }

        [TestMethod]
        public void CheckIfClickActionsAreAssignedByControllerToShipComponent()
        {
            var mockController = new Mock<IController>();
            var mockShipComponent = new Mock<IWorldObject>();
            mockShipComponent.SetupSet(component => component.LeftClickAction = It.IsAny<ShipComponentSelect>()).Verifiable();
            mockShipComponent.SetupSet(component => component.RightClickAction = It.IsAny<ShipComponentCancel>()).Verifiable();

            var position = new Coordinate(1, 3);
            var blueprintBuilderControlAssigner = new ControlAssigner(mockController.Object);
            blueprintBuilderControlAssigner.AssignShipComponentControl(mockShipComponent.Object, position);

            mockShipComponent.VerifySet(component => component.LeftClickAction = It.IsAny<ShipComponentSelect>());
            mockShipComponent.VerifySet(component => component.RightClickAction = It.IsAny<ShipComponentCancel>());
        }
    }
}
