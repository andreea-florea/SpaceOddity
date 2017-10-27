using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewInterface;
using Moq;
using Game.Interfaces;
using Geometry;
using ViewModel.Fancy;
using System.Collections.Generic;
using NaturalNumbersMath;

namespace ViewModel.Tests
{
    [TestClass]
    public class BlueprintBuilderFancyViewModelTest
    {
        private Mock<IDetailsViewUpdater> mockBlockDetailsViewUpdater;
        private BlueprintBuilderFancyViewModel fancyViewModel;

        [TestInitialize]
        public void Initialize()
        {
            var list = new List<IDetailsViewUpdater>();
            mockBlockDetailsViewUpdater = new Mock<IDetailsViewUpdater>();
            list.Add(mockBlockDetailsViewUpdater.Object);
            fancyViewModel = new BlueprintBuilderFancyViewModel(list);
        }

        [TestMethod]
        public void CheckIfDetailsUpdaterIsCalled()
        {
            var mockBlueprintBuilder = new Mock<IBlueprintBuilder>();
            fancyViewModel.BlockCreated(mockBlueprintBuilder.Object, 5, 4);
            mockBlockDetailsViewUpdater.Verify(updater => updater.UpdateDetails(new Coordinate(4, 5)), Times.Once());
        }
    }
}
