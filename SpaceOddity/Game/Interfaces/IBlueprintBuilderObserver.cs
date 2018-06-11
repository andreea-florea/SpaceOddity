using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlueprintBuilderObserver : IBlueprintObserver
    {
        void RestrictorAdded(IBlueprintBuilder blueprintBuilder, Coordinate position);
        void RestrictorRemoved(IBlueprintBuilder blueprintBuilder, Coordinate position);
        void BlockFactoryIndexChanged(IBlueprintBuilder blueprintBuilder, int index);
    }
}
