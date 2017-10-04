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
        void ErrorBlockNotCreated(IBlueprintBuilder blueprintBuilder, int y, int x);

        void BlockDeleted(IBlueprintBuilder blueprintBuilder, int y, int x);
        void ErrorBlockNotDeleted(IBlueprintBuilder blueprintBuilder, int y, int x);
    }
}
