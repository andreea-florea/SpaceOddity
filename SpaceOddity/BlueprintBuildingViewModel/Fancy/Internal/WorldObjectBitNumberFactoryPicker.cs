using Algorithms;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel;

namespace BlueprintBuildingViewModel.Fancy.Iternal
{
    internal class WorldObjectBitNumberFactoryPicker : IFacingContextWorldObjectFactory
    {
        private IFactory<IWorldObject>[] factories;
        private IBitNumberGenerator numberGenerator;

        public WorldObjectBitNumberFactoryPicker(
            IFactory<IWorldObject>[] factories, IBitNumberGenerator numberGenerator)
        {
            this.factories = factories;
            this.numberGenerator = numberGenerator;
        }

        public IWorldObject Create(FacingPosition position)
        {
            var index = numberGenerator.GenerateNumber(position).ToInt();
            return factories[index].Create();
        }
    }
}
