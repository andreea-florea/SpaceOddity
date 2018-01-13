using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueprintBuildingViewModel.Fancy
{
    public interface IDetailsViewUpdater
    {
        void UpdateDetails(Coordinate updatePosition);
    }
}
