using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IBlueprintBuilderObserver
    {
        void BlockCreated(IBlueprintBuilder blueprintBuilder, int y, int x);
    }
}
