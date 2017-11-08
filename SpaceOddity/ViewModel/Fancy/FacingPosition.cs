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

        public Coordinate Right 
        { 
            get
            {
                return Forward.RotateQuarterCircleRight();
            }
        }
        public Coordinate Position { get; private set; }

        public FacingPosition(Coordinate forward, Coordinate position) : this()
        {
            Forward = forward;
            Position = position;
        }

        public FacingPosition GetGlobal(Coordinate parentPosition)
        {
            return new FacingPosition(Forward, Position + parentPosition);
        }
    }
}
