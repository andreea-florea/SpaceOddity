using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Fancy.Iternal
{
    internal interface IBitNumberGenerator
    {
        bool[] GenerateNumber(Coordinate position, Coordinate facing);
    }
}
