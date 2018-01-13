using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel;

namespace BlueprintBuildingViewModel.Fancy.Iternal
{
    internal interface IFacingContextWorldObjectFactory
    {
        IWorldObject Create(FacingPosition position);
    }
}
