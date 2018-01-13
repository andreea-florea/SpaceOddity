using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlueprintBuildingViewModel.Fancy;
using NaturalNumbersMath;
using ViewInterface;
using Moq;
using BlueprintBuildingViewModel.Fancy.Iternal;
using Algorithm;
using ViewModel;

namespace BlueprintBuildingViewModel.Tests.Fancy
{
    [TestClass]
    public class WorldObjectBitNumberFactoryPickerTest
    {
        private Mock<IFactory<IWorldObject>> mockFirstFactory;
        private Mock<IFactory<IWorldObject>> mockSecondFactory;
        private Mock<IWorldObject> mockObject;
        private Mock<IBitNumberGenerator> mockNumberGenerator;
        private WorldObjectBitNumberFactoryPicker factoryPicker;

        [TestInitialize]
        public void Initialize()
        {
            mockFirstFactory = new Mock<IFactory<IWorldObject>>();
            mockSecondFactory = new Mock<IFactory<IWorldObject>>();
            mockObject = new Mock<IWorldObject>();
            var factories = new IFactory<IWorldObject>[2];
            factories[0] = mockFirstFactory.Object;
            factories[1] = mockSecondFactory.Object;
            mockNumberGenerator = new Mock<IBitNumberGenerator>();
            factoryPicker = new WorldObjectBitNumberFactoryPicker(factories, mockNumberGenerator.Object);
        }

        [TestMethod]
        public void FactoryPicksFirstBaseFactoryCorrectly()
        {
            var facingPosition = new FacingPosition(new Coordinate(1, 2), new Coordinate(3, 1));
            mockFirstFactory.Setup(factory => factory.Create()).Returns(mockObject.Object);
            mockNumberGenerator.Setup(generator => generator.GenerateNumber(facingPosition))
                .Returns(new bool[] { false });

            var worldObject = factoryPicker.Create(facingPosition);

            Assert.AreEqual(mockObject.Object, worldObject);
        }

        [TestMethod]
        public void FactoryPicksSecondBaseFactoryCorrectly()
        {
            var facingPosition = new FacingPosition(new Coordinate(1, 0), new Coordinate(3, 4));
            mockSecondFactory.Setup(factory => factory.Create()).Returns(mockObject.Object);
            mockNumberGenerator.Setup(generator => generator.GenerateNumber(facingPosition))
                .Returns(new bool[] { true });

            var worldObject = factoryPicker.Create(facingPosition);

            Assert.AreEqual(mockObject.Object, worldObject);
        }
    }
}
