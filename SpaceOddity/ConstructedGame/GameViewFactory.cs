using Game;
using Game.Interfaces;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel;
using ViewModel.Interfaces;

namespace ConstructedGame
{
    public class GameViewFactory
    {
        public void CreateBlueprintBuilderView(IWorldObjectFactory tileObjectFactory,
            IWorldObjectFactory blockObjectFactory, IRectangleSection fullRectangle)
        {
            var observableBlueprintBuilder = CreateBlueprintBuilder();
            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder, fullRectangle);
            var controller = new BlueprintBuilderController();

            var blueprintViewModelFactory =
                new BlueprintBuilderViewModelFactory(tileObjectFactory, blockObjectFactory, controller);
            blueprintViewModelFactory.CreateViewModel(observableBlueprintBuilder, fittingRectangle);
        }

        private IObservableBlueprintBuilder CreateBlueprintBuilder()
        {
            var width = 10;
            var height = 10;

            var blueprint = new IBlock[height, width];
            var blockFactory = new BlockFactory(1);
            var blueprintBuilder = new BlueprintBuilder(blueprint, blockFactory);
            return new ObservableBlueprintBuilder(blueprintBuilder);
        }

        private AspectRatioRectangleSection CreateViewRectangle(
            IObservableBlueprintBuilder observableBlueprintBuilder, IRectangleSection containingRectangle)
        {
            return new AspectRatioRectangleSection(
                new Vector2(observableBlueprintBuilder.Width, observableBlueprintBuilder.Height), containingRectangle);
        }
    }
}
