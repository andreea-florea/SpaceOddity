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
        public void CheckIfDetailsUpdaterIsCalledOnBlockCreated()
        {
            var position = new Coordinate(4, 5);
            var mockBlueprint = new Mock<IBlueprint>();
            fancyViewModel.BlockCreated(mockBlueprint.Object, position);
            mockBlockDetailsViewUpdater.Verify(updater => updater.UpdateDetails(position), Times.Once());
        }

        [TestMethod]
        public void CheckIfDetailsUpdaterIsCalledOnBlockDeleted()
        {
            var position = new Coordinate(4, 5);
            var mockBlueprint = new Mock<IBlueprint>();
            fancyViewModel.BlockDeleted(mockBlueprint.Object, position);
            mockBlockDetailsViewUpdater.Verify(updater => updater.UpdateDetails(position), Times.Once());
        }
    }
}
