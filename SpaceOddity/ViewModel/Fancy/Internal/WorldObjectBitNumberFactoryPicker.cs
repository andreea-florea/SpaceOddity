using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.Fancy.Iternal
{
    internal class WorldObjectBitNumberFactoryPicker : IFacingContextWorldObjectFactory
    {
        private IWorldObjectFactory[] factories;
        private IBitNumberGenerator numberGenerator;

        public WorldObjectBitNumberFactoryPicker(IWorldObjectFactory[] factories, IBitNumberGenerator numberGenerator)
        {
            this.factories = factories;
            this.numberGenerator = numberGenerator;
        }

        public IWorldObject CreateObject(FacingPosition position)
        {
            var index = numberGenerator.GenerateNumber(position).ToInt();
            return factories[index].CreateObject();
        }
    }
}
