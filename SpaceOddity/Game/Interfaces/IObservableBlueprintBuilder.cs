using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Interfaces
{
    public interface IObservableBlueprintBuilder : IBlueprintBuilder
    {
        void AttachObserver(IBlueprintBuilderObserver observer);
    }
}
