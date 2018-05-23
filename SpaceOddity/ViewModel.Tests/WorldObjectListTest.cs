using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel.Tests
{
    [TestClass]
    public class WorldObjectListTest
    {
        [TestMethod]
        public void CanAddWorldObjectToList()
        {
            var list = new WorldObjectList<IWorldObject>();
            var mockObjects = new List<IWorldObject>();
            mockObjects.Add(new Mock<IWorldObject>().Object);
            mockObjects.Add(new Mock<IWorldObject>().Object);

            list.AddRange(mockObjects);
            Assert.AreEqual(mockObjects[0], list[0]);
            Assert.AreEqual(mockObjects[1], list[1]);
        }

        [TestMethod]
        public void WorldObjectDeletesWhenRemovedByClearingFromList()
        {
            var list = new WorldObjectList<IWorldObject>();
            var mockObjects = new List<IWorldObject>();
            var mockObject1 = new Mock<IWorldObject>();
            var mockObject2 = new Mock<IWorldObject>();
            mockObjects.Add(mockObject1.Object);
            mockObjects.Add(mockObject2.Object);

            list.AddRange(mockObjects);
            list.Clear();

            mockObject1.Verify(mock => mock.Delete(), Times.Once());
            mockObject2.Verify(mock => mock.Delete(), Times.Once());
        }

        [TestMethod]
        public void WorldObjectIsEnumerable()
        {
            var list = new WorldObjectList<IWorldObject>();
            var mockObjects = new List<IWorldObject>();
            mockObjects.Add(new Mock<IWorldObject>().Object);
            mockObjects.Add(new Mock<IWorldObject>().Object);
            list.AddRange(mockObjects);

            Assert.AreEqual(2, list.Count());
        }

        [TestMethod]
        public void CanCountTheAmountOfWorldObjects()
        {
            var list = new WorldObjectList<IWorldObject>();
            var mockObjects = new List<IWorldObject>();
            mockObjects.Add(new Mock<IWorldObject>().Object);
            mockObjects.Add(new Mock<IWorldObject>().Object);

            list.AddRange(mockObjects);
            Assert.AreEqual(2, list.Count);
        }
    }
}
