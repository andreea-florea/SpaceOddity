using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;
using NaturalNumbersMath;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderControllerTest
    {
        [TestMethod]
        public void CreateBlocksOnTileSelect()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            var controller = new BlueprintBuilderController(mockBlueprintBuilder.Object);
            var position = new Coordinate(4, 2);
            controller.TileSelect(position);
            mockBlueprintBuilder.Verify(builder => builder.CreateBlock(position));
        }

        [TestMethod]
        public void DeletesBlockOnBlockCancel()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            var controller = new BlueprintBuilderController(mockBlueprintBuilder.Object);
            var position = new Coordinate(4, 2);
            controller.BlockCancel(position);
            mockBlueprintBuilder.Verify(builder => builder.DeleteBlock(position));
        }

        [TestMethod]
        public void CreatesShipComponentOnBlockSelect()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            var controller = new BlueprintBuilderController(mockBlueprintBuilder.Object);
            var position = new Coordinate(4, 2);
            controller.BlockSelect(position);
            mockBlueprintBuilder.Verify(builder => builder.AddShipComponent(position));
        }
    }
}
