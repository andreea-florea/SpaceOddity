using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Game.Interfaces;
using NaturalNumbersMath;
using Game.Enums;

namespace Game.Tests
{
    [TestClass]
    public class JetEngineTest
    {
        private IBlock[,] blocks;
        private Mock<IBlock> mockBlock;
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private EdgeType edgeTypeColumn;
        private EdgeType edgeTypeLine;
        private JetEngine jetEngine;

        [TestInitialize]
        public void Init()
        {
            edgeTypeColumn = EdgeType.DOWN;
            edgeTypeLine = EdgeType.LEFT;
            blocks = new Block[9, 10];
            mockBlock = new Mock<IBlock>();
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
        }

        [TestMethod]
        public void CheckIfCanCreateBlockIfBlockToBeCreatedNotOnTheSameLineOrTheSameColumn()
        {
            jetEngine = new JetEngine(mockBlock.Object, edgeTypeLine);
            var newBlockPosition = new Coordinate(4, 5);

            mockBlock.SetupGet(x => x.Position).Returns(new Coordinate(3, 4));

            Assert.IsTrue(jetEngine.CanCreateBlock(newBlockPosition));
        }

        [TestMethod]
        public void CheckIfCanCreateBlockIfBlockToBeCreatedOnTheSameColumnOutsideJetRange()
        {
            jetEngine = new JetEngine(mockBlock.Object, edgeTypeColumn);
            var newBlockPosition = new Coordinate(4, 3);

            mockBlock.SetupGet(x => x.Position).Returns(new Coordinate(4, 5));

            Assert.IsTrue(jetEngine.CanCreateBlock(newBlockPosition));
        }

        [TestMethod]
        public void CheckIfCanCreateBlockIfBlockToBeCreatedOnTheSameLineOutsideJetRange()
        {
            jetEngine = new JetEngine(mockBlock.Object, edgeTypeLine);
            var newBlockPosition = new Coordinate(6, 5);

            mockBlock.SetupGet(x => x.Position).Returns(new Coordinate(4, 5));

            Assert.IsTrue(jetEngine.CanCreateBlock(newBlockPosition));
        }

        [TestMethod]
        public void CheckThatBlockCannotBeCreatedIfBlockToBeCreatedIsWithinJetRangeOnTheSameLine()
        {
            jetEngine = new JetEngine(mockBlock.Object, edgeTypeLine);
            var newBlockPosition = new Coordinate(2, 5);

            mockBlock.SetupGet(x => x.Position).Returns(new Coordinate(4, 5));

            Assert.IsFalse(jetEngine.CanCreateBlock(newBlockPosition));
        }

        [TestMethod]  
        public void CheckThatBlockCannotBeCreatedIfBlockToBeCreatedIsWithinJetRangeOnTheSameColumn()
        {
            jetEngine = new JetEngine(mockBlock.Object, edgeTypeColumn);
            var newBlockPosition = new Coordinate(4, 6);

            mockBlock.SetupGet(x => x.Position).Returns(new Coordinate(4, 5));

            Assert.IsFalse(jetEngine.CanCreateBlock(newBlockPosition));
        }

        [TestMethod]
        public void CheckIfAdditionalSetupsTriesToAddJetEngineToBlueprintBuilderBlockRestrictorList()
        {
            jetEngine = new JetEngine(mockBlock.Object, edgeTypeColumn);

            jetEngine.AdditionalSetups(mockBlueprintBuilder.Object);

            mockBlueprintBuilder.Verify(x => x.AddRestrictor(jetEngine), Times.Once());
        }

        [TestMethod]
        public void CheckIfRemoveAdditionalSetupsTriesToDeleteJetEngineFromBlueprintBuilderBlockRestrictorList()
        {
            jetEngine = new JetEngine(mockBlock.Object, edgeTypeColumn);

            jetEngine.RemoveAdditionalSetups(mockBlueprintBuilder.Object);

            mockBlueprintBuilder.Verify(x => x.RemoveRestrictor(jetEngine), Times.Once());
        }
    }
}
