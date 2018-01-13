using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueprintBuildingViewModel.Fancy.Iternal
{
    internal interface IBitNumberGenerator
    {
        bool[] GenerateNumber(FacingPosition position);
    }
}
