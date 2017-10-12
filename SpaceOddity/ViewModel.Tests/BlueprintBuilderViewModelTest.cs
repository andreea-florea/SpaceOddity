using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel.Interfaces;
using Moq;
using ViewInterface;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderViewModelTest
    {
        [TestMethod]
        public void BlueprintHasCorrectSlotsAssigned()
        {
            var mockSlot = new Mock<IWorldObject>();
            var slots = new IWorldObject[5,6];
            slots[3, 1] = mockSlot.Object;

            var blueprintBuilderViewModel = new BlueprintBuilderViewModel(slots);
            Assert.AreEqual(6, blueprintBuilderViewModel.Width);
            Assert.AreEqual(5, blueprintBuilderViewModel.Height);
            Assert.AreEqual(mockSlot.Object, blueprintBuilderViewModel.GetSlot(3, 1));
        }
    }
}
