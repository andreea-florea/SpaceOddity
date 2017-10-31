using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturalNumbersMath;

namespace Geometry.Tests
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void RectangleHasCorrectCornersAssigned()
        {
            var topLeftCorner = new Vector2(4, 5);
            var bottomRightCorner = new Vector2(6, 7);
            var rectangle = new Rectangle(topLeftCorner, bottomRightCorner);

            Assert.AreEqual(4, rectangle.TopLeftCorner.X);
            Assert.AreEqual(5, rectangle.TopLeftCorner.Y);
            Assert.AreEqual(6, rectangle.BottomRightCorner.X);
            Assert.AreEqual(7, rectangle.BottomRightCorner.Y);
        }

        [TestMethod]
        public void RectangleHasCorrectDimensions()
        {
            var topLeftCorner = new Vector2(4, 5);
            var bottomRightCorner = new Vector2(6, 8);
            var rectangle = new Rectangle(topLeftCorner, bottomRightCorner);

            Assert.AreEqual(2, rectangle.Dimensions.X);
            Assert.AreEqual(3, rectangle.Dimensions.Y);
        }


        [TestMethod]
        public void RectangleHasCorrectCenter()
        {
            var topLeftCorner = new Vector2(4, 5);
            var bottomRightCorner = new Vector2(6, 9);
            var rectangle = new Rectangle(topLeftCorner, bottomRightCorner);

            Assert.AreEqual(5, rectangle.Center.X);
            Assert.AreEqual(7, rectangle.Center.Y);
        }


        [TestMethod]
        public void RectangleSplitsInCorrectAmountOfRectangles()
        {
            var topLeftCorner = new Vector2(3, 2);
            var bottomRightCorner = new Vector2(7, 8);
            var rectangle = new Rectangle(topLeftCorner, bottomRightCorner);

            var splits = rectangle.Split(new Coordinate(2, 3));
            Assert.AreEqual(3, splits.GetLength(0));
            Assert.AreEqual(2, splits.GetLength(1));
        }

        [TestMethod]
        public void RectangleSplitsAreCorrectlyProportionate()
        {
            var topLeftCorner = new Vector2(1, 0);
            var bottomRightCorner = new Vector2(5, 8);
            var rectangle = new Rectangle(topLeftCorner, bottomRightCorner);

            var splits = rectangle.Split(new Coordinate(2, 2));
            Assert.AreEqual(1, splits[0, 0].TopLeftCorner.X);
            Assert.AreEqual(0, splits[0, 0].TopLeftCorner.Y);
            Assert.AreEqual(3, splits[0, 0].BottomRightCorner.X);
            Assert.AreEqual(4, splits[0, 0].BottomRightCorner.Y);
            Assert.AreEqual(3, splits[0, 1].TopLeftCorner.X);
            Assert.AreEqual(0, splits[0, 1].TopLeftCorner.Y);
            Assert.AreEqual(5, splits[0, 1].BottomRightCorner.X);
            Assert.AreEqual(4, splits[0, 1].BottomRightCorner.Y);
            Assert.AreEqual(1, splits[1, 0].TopLeftCorner.X);
            Assert.AreEqual(4, splits[1, 0].TopLeftCorner.Y);
            Assert.AreEqual(3, splits[1, 0].BottomRightCorner.X);
            Assert.AreEqual(8, splits[1, 0].BottomRightCorner.Y);
            Assert.AreEqual(3, splits[1, 1].TopLeftCorner.X);
            Assert.AreEqual(4, splits[1, 1].TopLeftCorner.Y);
            Assert.AreEqual(5, splits[1, 1].BottomRightCorner.X);
            Assert.AreEqual(8, splits[1, 1].BottomRightCorner.Y);
        }
    }
}
