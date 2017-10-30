﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalNumbersMath.Tests
{
    [TestClass]
    public class ArrayExtensionsTest
    {
        [TestMethod]
        public void CanAccessMemberOfArrayWithExtension()
        {
            int[,] array = new int[3, 4];
            array[2, 1] = 5;
            Assert.AreEqual(5, array.Get(new Coordinate(1, 2)));
        }

        [TestMethod]
        public void CanSetMemberOfArrayWithExtension()
        {
            int[,] array = new int[3, 4];
            array.Set(new Coordinate(1, 2), 5);
            Assert.AreEqual(5, array[2, 1]);
        }
    }
}