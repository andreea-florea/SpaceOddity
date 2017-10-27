using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.Fancy
{
    public interface IFacingContextWorldObjectFactory
    {
        IWorldObject CreateObject(Coordinate position, Coordinate facing);
    }
}
