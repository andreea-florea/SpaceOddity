using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.DataStructures;

namespace ViewModel
{
    public interface IBlueprintBuilderTableHighlighter
    {
        void ActivatePipeLink(CoordinatePair edge);
        void DeactivateAll();
    }
}
