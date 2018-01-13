using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Interfaces;
using Moq;
using Geometry;
using ViewInterface;
using NaturalNumbersMath;
using BlueprintBuildingViewModel.Controller;
using Algorithm;
using ViewModel;

namespace BlueprintBuildingViewModel.Tests
{
    [TestClass]
    public class ViewModelTilesFactoryTest
    {
        private Mock<IBlueprintBuilder> mockBlueprintBuilder;
        private Mock<IRectangleSection> mockRectangleSection;
        private Mock<IFactory<IActivateableWorldObject>> mockTileObjectFactory;
        private Mock<IActivateableWorldObject> mockTile;
        private Mock<IBlueprintBuilderControlAssigner> mockController;
        private ViewModelTilesFactory tilesFactory;

        [TestInitialize]
        public void Initialize()
        {
            mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            mockRectangleSection = new Mock<IRectangleSection>();
            mockTileObjectFactory = new Mock<IFactory<IActivateableWorldObject>>();
            mockTile = new Mock<IActivateableWorldObject>();
            mockTile.SetupAllProperties();
            mockController = new Mock<IBlueprintBuilderControlAssigner>();
            tilesFactory = new ViewModelTilesFactory(mockTileObjectFactory.Object);
        }

        [TestMethod]
        public void CorrectAmountOfTilesAreCreated()
        {
            mockTileObjectFactory.Setup(factory => factory.Create()).Returns(mockTile.Object);
            mockRectangleSection.Setup(section => section.Section).Returns(new Rectangle());

            var tiles = tilesFactory.CreateTiles(new Coordinate(7, 3), mockRectangleSection.Object);
            Assert.AreEqual(3, tiles.Height());
            Assert.AreEqual(7, tiles.Width());
        }

        [TestMethod]
        public void TilesAreCreatedAtCorrectPosition()
        {
            var mockTestTile = new Mock<IActivateableWorldObject>();
            mockTestTile.SetupAllProperties();

            mockTileObjectFactory.SetupSequence(factory => factory.Create())
                .Returns(mockTile.Object)
                .Returns(mockTile.Object)
                .Returns(mockTile.Object)
                .Returns(mockTile.Object)
                .Returns(mockTestTile.Object)
                .Returns(mockTile.Object);

            mockRectangleSection.Setup(section => section.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(4, 8)));

            var tiles = tilesFactory.CreateTiles(new Coordinate(3, 2), mockRectangleSection.Object);
            Assert.AreEqual(2.5, mockTestTile.Object.Position.X);
            Assert.AreEqual(6.5, mockTestTile.Object.Position.Y);
        }

        [TestMethod]
        public void TilesAreCreatedWithCorrectScale()
        {
            mockTileObjectFactory.Setup(factory => factory.Create()).Returns(mockTile.Object);
            mockRectangleSection.Setup(section => section.Section).Returns(new Rectangle(new Vector2(1, 2), new Vector2(4, 8)));

            var tiles = tilesFactory.CreateTiles(new Coordinate(3, 2), mockRectangleSection.Object);
            Assert.AreEqual(1, mockTile.Object.Scale.X);
            Assert.AreEqual(3, mockTile.Object.Scale.Y);
        }
    }
}
