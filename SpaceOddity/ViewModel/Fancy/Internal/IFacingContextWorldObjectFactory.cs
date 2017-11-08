using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.Fancy.Iternal
{
    internal interface IFacingContextWorldObjectFactory
    {
        IWorldObject CreateObject(FacingPosition position);
    }
}
