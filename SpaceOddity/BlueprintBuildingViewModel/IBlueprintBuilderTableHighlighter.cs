using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueprintBuildingViewModel.DataStructures;

namespace BlueprintBuildingViewModel
{
    public interface IBlueprintBuilderTableHighlighter
    {
        void ActivatePipeLink(CoordinatePair edge);
        void DeactivateAll();
    }
}
