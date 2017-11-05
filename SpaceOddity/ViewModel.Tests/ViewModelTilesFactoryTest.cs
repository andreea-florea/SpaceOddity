using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;

using Geometry;
using ViewInterface;
using NaturalNumbersMath;

namespace ViewModel.Tests
{
    [TestClass]
    public class ViewModelTilesFactoryTest
    {
        private Mock<IObservableBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IRectangleSection> mockRectangleSection;
        private Mock<IWorldObjectFactory> mockTileObjectFactory;
        private Mock<IWorldObject> mockTile;
        private Mock<IBlueprintBuilderControlAssigner> mockController;
        private ViewModelTilesFactory tilesFactory;

        [TestInitialize]
        public void Initialize()
        {
            mockBlueprintBuilder = new Mock<IObservableBlueprintBuilder>();
            mockRectangleSection = new Mock<IRectangleSection>();
            mockTileObjectFactory = new Mock<IWorldObjectFactory>();
            mockTile = new Mock<IWorldObject>();
            mockTile.SetupAllProperties();
            mockController = new Mock<IBlueprintBuilderControlAssigner>();
            tilesFactory = new ViewModelTilesFactory(mockTileObjectFactory.Object);
        }

        [TestMethod]
        public void CorrectAmountOfTilesAreCreated()
        {
            mockTileObjectFactory.Setup(factory => factory.CreateObject()).Returns(mockTile.Object);
            mockRectangleSection.Setup(section => section.Section).Returns(new Rectangle());

            var tiles = tilesFactory.CreateTiles(mockController.Object, new Coordinate(7, 3), mockRectangleSection.Object);
            Assert.AreEqual(3, tiles.GetLength(0));
            Assert.AreEqual(7, tiles.GetLength(1));
        }

        [TestMethod]
        public void TilesAreCreatedAtCorrectPosition()
        {
            var mockTestTile = new Mock<IWorldObject>();
            mockTestTile.SetupAllProperties();

            mockTileObjectFactory.SetupSequence(factory => factory.CreateObject())
                .Returns(mockTile.Object)
                .Returns(mockTile.Object)
                .Returns(mockTile.Object)
                .Returns(mockTile.Object)
                .Returns(mockTestTile.Object)
                .Returns(mockTile.Object);

            mockRectangleSection.Setup(section => section.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(4, 8)));

            var tiles = tilesFactory.CreateTiles(mockController.Object, new Coordinate(3, 2), mockRectangleSection.Object);
            Assert.AreEqual(2.5, mockTestTile.Object.Position.X);
            Assert.AreEqual(6.5, mockTestTile.Object.Position.Y);
        }

        [TestMethod]
        public void TilesAreCreatedWithCorrectScale()
        {
            mockTileObjectFactory.Setup(factory => factory.CreateObject()).Returns(mockTile.Object);
            mockRectangleSection.Setup(section => section.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(4, 8)));

            var tiles = tilesFactory.CreateTiles(mockController.Object, new Coordinate(3, 2), mockRectangleSection.Object);
            Assert.AreEqual(1, mockTile.Object.Scale.X);
            Assert.AreEqual(3, mockTile.Object.Scale.Y);
        }

        [TestMethod]
        public void CheckIfViewModelTilesAreAssignedControl()
        {
            var mockTestTile = new Mock<IWorldObject>();
            mockTestTile.SetupAllProperties();

            mockTileObjectFactory.SetupSequence(factory => factory.CreateObject())
                .Returns(mockTile.Object)
                .Returns(mockTile.Object)
                .Returns(mockTestTile.Object)
                .Returns(mockTile.Object);

            mockRectangleSection.Setup(section => section.Section).Returns(new Rectangle());

            var tiles = tilesFactory.CreateTiles(mockController.Object, new Coordinate(2, 2), mockRectangleSection.Object);
            mockController.Verify(controller => controller.AssignTileControl(mockTile.Object, new Coordinate(0, 0)), Times.Once());
            mockController.Verify(controller => controller.AssignTileControl(mockTile.Object, new Coordinate(1, 0)), Times.Once());
            mockController.Verify(controller => controller.AssignTileControl(mockTestTile.Object, new Coordinate(0, 1)), Times.Once());
            mockController.Verify(controller => controller.AssignTileControl(mockTile.Object, new Coordinate(1, 1)), Times.Once());
        }
    }
}
