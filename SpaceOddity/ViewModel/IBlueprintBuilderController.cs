using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public interface IBlueprintBuilderController
    {
        void TileSelect(Coordinate position);
        void BlockCancel(Coordinate position);
    }
}
