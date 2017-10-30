﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Fancy;
using Geometry;
using NaturalNumbersMath;

namespace ViewModel.Tests.Fancy
{
    [TestClass]
    public class FacingPositionTest
    {
        [TestMethod]
        public void CornerUpdatePropertiesAreSetCorrectly()
        {
            var cornerUpdate = new FacingPosition(new Coordinate(1, 0), new Coordinate(5, 4));
            Assert.AreEqual(1, cornerUpdate.Forward.X);
            Assert.AreEqual(0, cornerUpdate.Forward.Y);
            Assert.AreEqual(5, cornerUpdate.RelativePosition.X);
            Assert.AreEqual(4, cornerUpdate.RelativePosition.Y);
        }
    }
}