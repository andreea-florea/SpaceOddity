using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geometry.Tests
{
    [TestClass]
    public class Vector2Test
    {
        [TestMethod]
        public void CheckIfVector2HasCorrectCoordinates()
        {
            var vector = new Vector2(7.0, 3.0);
            Assert.AreEqual(7.0, vector.X);
            Assert.AreEqual(3.0, vector.Y);
        }

        [TestMethod]
        public void CheckIfTwoVectorsAddCorrectly()
        {
            var firstVector = new Vector2(5, 6);
            var secondVector = new Vector2(1, 3);
            var addedVector = firstVector + secondVector;
            Assert.AreEqual(6, addedVector.X);
            Assert.AreEqual(9, addedVector.Y);
        }

        [TestMethod]
        public void CheckIfTwoVectorsSubtractCorrectly()
        {
            var firstVector = new Vector2(5, 6);
            var secondVector = new Vector2(1, 3);
            var addedVector = firstVector - secondVector;
            Assert.AreEqual(4, addedVector.X);
            Assert.AreEqual(3, addedVector.Y);
        }

        [TestMethod]
        public void CheckIfOneVectorGetsScalledCorrectly()
        {
            var vector = new Vector2(3, 4);
            var scaledVector = 2 * vector * 3;
            Assert.AreEqual(18, scaledVector.X);
            Assert.AreEqual(24, scaledVector.Y);
        }

        [TestMethod]
        public void CheckIfOneVectorGetsDividedCorrectly()
        {
            var vector = new Vector2(3, 9);
            var scaledVector = vector / 3;
            Assert.AreEqual(1, scaledVector.X);
            Assert.AreEqual(3, scaledVector.Y);
        }

        [TestMethod]
        public void CheckIfXAxisProjectionIsCorrect()
        {
            var vector = new Vector2(4, 5);
            var xVector = vector.XProjection;
            Assert.AreEqual(4, xVector.X);
            Assert.AreEqual(0, xVector.Y);
        }


        [TestMethod]
        public void CheckIfYAxisProjectionIsCorrect()
        {
            var vector = new Vector2(4, 5);
            var yVector = vector.YProjection;
            Assert.AreEqual(0, yVector.X);
            Assert.AreEqual(5, yVector.Y);
        }

        [TestMethod]
        public void CheckIfDividingAVectorByAnotherOneIsDoneMemberByMember()
        {
            var vector = new Vector2(8, 6);
            var division = new Vector2(2, 3);
            var dividedVector = vector.Divide(division);
            Assert.AreEqual(4, dividedVector.X);
            Assert.AreEqual(2, dividedVector.Y);
        }
    }
}
