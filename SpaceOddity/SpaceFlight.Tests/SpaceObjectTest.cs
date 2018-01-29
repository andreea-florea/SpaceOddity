using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geometry;
using Moq;

namespace SpaceFlight.Tests
{
    [TestClass]
    public class SpaceObjectTest
    {
        [TestMethod]
        public void SpaceObjectPropertiesAreAssignedCorrectly()
        {
            var spaceObject = new SpaceObject(new Vector2(3, 5), new Vector2(1, 0));

            Assert.AreEqual(new Vector2(3, 5), spaceObject.Position);
            Assert.AreEqual(new Vector2(1, 0), spaceObject.Rotation);
        }

        [TestMethod]
        public void SpaceObjectObserverGetsUpdated()
        {
            var mockObserver = new Mock<ISpaceObjectObserver>();
            var spaceObject = new SpaceObject(new Vector2(3, 2), new Vector2(1, 0));

            spaceObject.AttachObserver(mockObserver.Object);
            spaceObject.Position = new Vector2(1, 7);
            spaceObject.Rotation = new Vector2(1, 4);

            mockObserver.Verify(observer => observer.ObjectUpdated(), Times.Exactly(2));
        }
    }
}
