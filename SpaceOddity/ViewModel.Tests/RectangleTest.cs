using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using Geometry;
using NaturalNumbersMath;

namespace ViewModel.Tests
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void RectangleHasCorrectCornersAssigned()
        {
            var bottomLeftCorner = new Vector2(4, 5);
            var topRightCorner = new Vector2(6, 7);
            var rectangle = new Rectangle(bottomLeftCorner, topRightCorner);

            Assert.AreEqual(4, rectangle.BottomLeftCorner.X);
            Assert.AreEqual(5, rectangle.BottomLeftCorner.Y);
            Assert.AreEqual(6, rectangle.TopRightCorner.X);
            Assert.AreEqual(7, rectangle.TopRightCorner.Y);
        }

        [TestMethod]
        public void RectangleHasCorrectDimensions()
        {
            var bottomLeftCorner = new Vector2(4, 5);
            var topRightCorner = new Vector2(6, 8);
            var rectangle = new Rectangle(bottomLeftCorner, topRightCorner);

            Assert.AreEqual(2, rectangle.Dimensions.X);
            Assert.AreEqual(3, rectangle.Dimensions.Y);
        }

        [TestMethod]
        public void RectangleSplitsInCorrectAmountOfRectangles()
        {
            var bottomLeftCorner = new Vector2(3, 2);
            var topRightCorner = new Vector2(7, 8);
            var rectangle = new Rectangle(bottomLeftCorner, topRightCorner);

            var splits = rectangle.Split(new Coordinate(2, 3));
            Assert.AreEqual(3, splits.Height());
            Assert.AreEqual(2, splits.Width());
        }

        [TestMethod]
        public void RectangleSplitsAreCorrectlyProportionate()
        {
            var bottomLeftCorner = new Vector2(1, 0);
            var topRightCorner = new Vector2(5, 8);
            var rectangle = new Rectangle(bottomLeftCorner, topRightCorner);

            var splits = rectangle.Split(new Coordinate(2, 2));
            Assert.AreEqual(1, splits[0, 0].BottomLeftCorner.X);
            Assert.AreEqual(0, splits[0, 0].BottomLeftCorner.Y);
            Assert.AreEqual(3, splits[0, 0].TopRightCorner.X);
            Assert.AreEqual(4, splits[0, 0].TopRightCorner.Y);
            Assert.AreEqual(3, splits[0, 1].BottomLeftCorner.X);
            Assert.AreEqual(0, splits[0, 1].BottomLeftCorner.Y);
            Assert.AreEqual(5, splits[0, 1].TopRightCorner.X);
            Assert.AreEqual(4, splits[0, 1].TopRightCorner.Y);
            Assert.AreEqual(1, splits[1, 0].BottomLeftCorner.X);
            Assert.AreEqual(4, splits[1, 0].BottomLeftCorner.Y);
            Assert.AreEqual(3, splits[1, 0].TopRightCorner.X);
            Assert.AreEqual(8, splits[1, 0].TopRightCorner.Y);
            Assert.AreEqual(3, splits[1, 1].BottomLeftCorner.X);
            Assert.AreEqual(4, splits[1, 1].BottomLeftCorner.Y);
            Assert.AreEqual(5, splits[1, 1].TopRightCorner.X);
            Assert.AreEqual(8, splits[1, 1].TopRightCorner.Y);
        }
    }
}
