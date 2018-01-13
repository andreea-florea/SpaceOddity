using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geometry;

namespace SpaceFlight.Tests
{
    [TestClass]
    public class SpaceObjectTest
    {
        [TestMethod]
        public void SpaceObjectPropertiesAreAssignedCorrectly()
        {
            var spaceObject = new SpaceObject(new Vector2(3, 5), new Vector2(1, 0),
                new Vector2(2, 3), new Vector2(6, 7));

            Assert.AreEqual(new Vector2(3, 5), spaceObject.Position);
            Assert.AreEqual(new Vector2(1, 0), spaceObject.Rotation);
            Assert.AreEqual(new Vector2(2, 3), spaceObject.TranslationalForce);
            Assert.AreEqual(new Vector2(6, 7), spaceObject.RotationalForce);
        }

        [TestMethod]
        public void CorrectlyAddForceToSpaceObject()
        {
            var spaceObject = new SpaceObject(new Vector2(3, 2), new Vector2(1, 0),
                new Vector2(), new Vector2());

        }
    }
}
