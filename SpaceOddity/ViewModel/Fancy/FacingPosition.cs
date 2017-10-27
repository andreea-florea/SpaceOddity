using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Fancy
{
    public struct FacingPosition
    {
        public Coordinate Forward { get; private set; }
        public Coordinate RelativePosition { get; private set; }

        public FacingPosition(Coordinate forward, Coordinate relativePosition) : this()
        {
            Forward = forward;
            RelativePosition = relativePosition;
        }
    }
}
