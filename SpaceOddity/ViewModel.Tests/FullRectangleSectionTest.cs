﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Interfaces;
using Geometry;
using Moq;

namespace ViewModel.Tests
{
    [TestClass]
    public class FullRectangleSectionTest
    {
        [TestMethod]
        public void CheckIfRectangleCornersAreReturnedCorrectly()
        {
            var rectangle = new Rectangle(new Vector2(1, 1), new Vector2(4, 7));
            var rectangleSection = new FullRectangleSection(rectangle);

            Assert.AreEqual(1, rectangleSection.Section.TopLeftCorner.X);
            Assert.AreEqual(1, rectangleSection.Section.TopLeftCorner.Y);
            Assert.AreEqual(4, rectangleSection.Section.BottomRightCorner.X);
            Assert.AreEqual(7, rectangleSection.Section.BottomRightCorner.Y);
        }
    }
}
