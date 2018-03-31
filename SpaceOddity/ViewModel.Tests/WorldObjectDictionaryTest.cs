using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel;

namespace ViewModel.Tests
{
    [TestClass]
    public class WorldObjectDictionaryTest
    {
        [TestMethod]
        public void CanAddWorldObjectToDictionary()
        {
            var dictionary = new WorldObjectDictionary<int, IWorldObject>();
            var mockObject = new Mock<IWorldObject>();

            dictionary.Add(3, mockObject.Object);
            Assert.AreEqual(mockObject.Object, dictionary[3]);
        }

        [TestMethod]
        public void WorldObjectDeletesWhenRemovedFromDictionary()
        {
            var dictionary = new WorldObjectDictionary<int, IWorldObject>();
            var mockObject = new Mock<IWorldObject>();

            dictionary.Add(3, mockObject.Object);
            dictionary.Remove(3);

            mockObject.Verify(mock => mock.Delete(), Times.Once());
        }
    }
}
