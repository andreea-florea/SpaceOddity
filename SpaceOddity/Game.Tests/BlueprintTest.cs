using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Game.Interfaces;
using NaturalNumbersMath;

namespace Game.Tests
{
    [TestClass]
    public class BlueprintTest
    {
        private Blueprint blueprint;
        private IBlock[,] blocks;
        private Mock<IBlock> mockBlock;

        [TestInitialize]
        public void Init()
        {
            blocks = new IBlock[6, 7];
            blueprint = new Blueprint(blocks);
            mockBlock = new Mock<IBlock>();
        }

        [TestMethod]
        public void CheckThatBlockIsSetCorrectly()
        {
            var position = new Coordinate(3, 4);
            blueprint.PlaceBlock(position, mockBlock.Object);
            Assert.AreEqual(mockBlock.Object, blueprint.GetBlock(position));
        }

        [TestMethod]
        public void CheckThatBlockIsRemoved()
        {
            var position = new Coordinate(3, 4);
            blueprint.PlaceBlock(position, mockBlock.Object);
            blueprint.RemoveBlock(position);
            Assert.AreEqual(null, blueprint.GetBlock(position));
        }

        [TestMethod]
        public void CheckThatHasBlockReturnsTrueWhenExistingBlockAtGivenPosition()
        {
            
        }
    }
}
