using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Fancy;
using NaturalNumbersMath;
using ViewInterface;
using Moq;
using ViewModel.Fancy.Iternal;

namespace ViewModel.Tests.Fancy
{
    [TestClass]
    public class WorldObjectBitNumberFactoryPickerTest
    {
        private Mock<IWorldObjectFactory> mockFirstFactory;
        private Mock<IWorldObjectFactory> mockSecondFactory;
        private Mock<IWorldObject> mockObject;
        private Mock<IBitNumberGenerator> mockNumberGenerator;
        private WorldObjectBitNumberFactoryPicker factoryPicker;

        [TestInitialize]
        public void Initialize()
        {
            mockFirstFactory = new Mock<IWorldObjectFactory>();
            mockSecondFactory = new Mock<IWorldObjectFactory>();
            mockObject = new Mock<IWorldObject>();
            var factories = new IWorldObjectFactory[2];
            factories[0] = mockFirstFactory.Object;
            factories[1] = mockSecondFactory.Object;
            mockNumberGenerator = new Mock<IBitNumberGenerator>();
            factoryPicker = new WorldObjectBitNumberFactoryPicker(factories, mockNumberGenerator.Object);
        }

        [TestMethod]
        public void FactoryPicksFirstBaseFactoryCorrectly()
        {
            var position = new Coordinate(3, 1);
            var facing = new Coordinate(1, 2);
            mockFirstFactory.Setup(factory => factory.CreateObject()).Returns(mockObject.Object);
            mockNumberGenerator.Setup(generator => generator.GenerateNumber(new Coordinate(3, 1), new Coordinate(1, 2)))
                .Returns(new bool[] { false });

            var worldObject = factoryPicker.CreateObject(position, facing);

            Assert.AreEqual(mockObject.Object, worldObject);
        }

        [TestMethod]
        public void FactoryPicksSecondBaseFactoryCorrectly()
        {
            var position = new Coordinate(3, 4);
            var facing = new Coordinate(1, 0);
            mockSecondFactory.Setup(factory => factory.CreateObject()).Returns(mockObject.Object);
            mockNumberGenerator.Setup(generator => generator.GenerateNumber(new Coordinate(3, 4), new Coordinate(1, 0)))
                .Returns(new bool[] { true });

            var worldObject = factoryPicker.CreateObject(position, facing);

            Assert.AreEqual(mockObject.Object, worldObject);
        }
    }
}
