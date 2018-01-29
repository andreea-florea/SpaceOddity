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
    internal class IgnoreFacingContextWorldObjectFactory : IFacingContextWorldObjectFactory
    {
        private IFactory<IWorldObject> baseFactory;

        public IgnoreFacingContextWorldObjectFactory(IFactory<IWorldObject> baseFactory)
        {
            this.baseFactory = baseFactory;
        }

        public IWorldObject Create(FacingPosition position)
        {
            return baseFactory.Create();
        }
    }
}
