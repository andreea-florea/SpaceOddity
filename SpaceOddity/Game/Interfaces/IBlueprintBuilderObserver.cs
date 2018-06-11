using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlueprintBuilderObserver : IBlueprintObserver
    {
        void RestrictorAdded(IBlueprintBuilder blueprintBuilder, IBlockRestrictor restrictor);
        void RestrictorRemoved(IBlueprintBuilder blueprintBuilder, IBlockRestrictor restrictor);
        void BlockFactoryIndexChanged(IBlueprintBuilder blueprintBuilder, int index);
    }
}
