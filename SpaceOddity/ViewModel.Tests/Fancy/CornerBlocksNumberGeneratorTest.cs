﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Fancy;
using Geometry;
using Game.Interfaces;
using Moq;
using NaturalNumbersMath;

namespace ViewModel.Tests.Fancy
{
    [TestClass]
    public class CornerBlocksNumberGeneratorTest
    {
        [TestMethod]
        public void CheckIfFrontBlockIsCountedTest()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(1, 3))).Returns(true);
            var cornerBlocksNumberGenerator = new CornerBlocksNumberGenerator(mockBlueprintBuilder.Object);

            var bitNumber = cornerBlocksNumberGenerator.GenerateNumber(new Coordinate(1, 2), new Coordinate(0, 1));
            Assert.AreEqual(true, bitNumber[0]);
            Assert.AreEqual(false, bitNumber[1]);
            Assert.AreEqual(false, bitNumber[2]);
        }

        [TestMethod]
        public void CheckIfRightBlockIsCountedTest()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 2))).Returns(true);
            var cornerBlocksNumberGenerator = new CornerBlocksNumberGenerator(mockBlueprintBuilder.Object);

            var bitNumber = cornerBlocksNumberGenerator.GenerateNumber(new Coordinate(1, 2), new Coordinate(0, 1));
            Assert.AreEqual(false, bitNumber[0]);
            Assert.AreEqual(true, bitNumber[1]);
            Assert.AreEqual(false, bitNumber[2]);
        }

        [TestMethod]
        public void CheckIfDiagonalBlockIsCountedTest()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockBlueprintBuilder.Setup(builder => builder.HasBlock(new Coordinate(2, 3))).Returns(true);
            var cornerBlocksNumberGenerator = new CornerBlocksNumberGenerator(mockBlueprintBuilder.Object);

            var bitNumber = cornerBlocksNumberGenerator.GenerateNumber(new Coordinate(1, 2), new Coordinate(0, 1));
            Assert.AreEqual(false, bitNumber[0]);
            Assert.AreEqual(false, bitNumber[1]);
            Assert.AreEqual(true, bitNumber[2]);
        }
    }
}
