using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public class FullRectangleSection : IRectangleSection
    {
        public Rectangle Section { get; private set; }

        public FullRectangleSection(Rectangle baseSection)
        {
            Section = baseSection;
        }
    }
}
