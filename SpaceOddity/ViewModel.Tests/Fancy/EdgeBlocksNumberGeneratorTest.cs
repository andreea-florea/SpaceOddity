using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Fancy;
using Geometry;
using Game.Interfaces;
using Moq;
using NaturalNumbersMath;

namespace ViewModel.Tests.Fancy
{
    [TestClass]
    public class EdgeBlocksNumberGeneratorTest
    {
        [TestMethod]
        public void CheckIfFrontBlockIsEmpty()
        {
            var mockBlock = new Mock<IBlock>();
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            var cornerBlocksNumberGenerator = new EdgeBlocksNumberGenerator(mockBlueprintBuilder.Object);

            var bitNumber = cornerBlocksNumberGenerator.GenerateNumber(new Coordinate(1, 2), new Coordinate(0, 1));
            Assert.AreEqual(false, bitNumber[0]);
        }

        [TestMethod]
        public void CountForFullFrontBlock()
        {
            var mockBlock = new Mock<IBlock>();
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockBlueprintBuilder.Setup(builder => builder.GetBlock(new Coordinate(1, 3))).Returns(mockBlock.Object);
            var cornerBlocksNumberGenerator = new EdgeBlocksNumberGenerator(mockBlueprintBuilder.Object);

            var bitNumber = cornerBlocksNumberGenerator.GenerateNumber(new Coordinate(1, 2), new Coordinate(0, 1));
            Assert.AreEqual(true, bitNumber[0]);
        }
    }
}
