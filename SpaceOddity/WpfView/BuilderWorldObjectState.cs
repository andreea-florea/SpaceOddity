using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfView
{
    public struct BuilderWorldObjectState
    {
        public ColorVector Fill { get; private set; }
        public ColorVector Border { get; private set; }

        public BuilderWorldObjectState(ColorVector fill, ColorVector border) : this()
        {
            this.Fill = fill;
            this.Border = border;
        }
    }
}
