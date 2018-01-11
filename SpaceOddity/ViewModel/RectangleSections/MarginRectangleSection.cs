using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public class MarginRectangleSection : IRectangleSection
    {
        private Vector2 margin;
        private IRectangleSection baseRectangleSection;

        public Rectangle Section
        {
            get 
            {
                var baseSection =  baseRectangleSection.Section;
                return new Rectangle(baseSection.BottomLeftCorner + margin, baseSection.TopRightCorner - margin);
            }
        }

        public MarginRectangleSection(Vector2 margin, IRectangleSection baseRectangleSection)
        {
            this.margin = margin;
            this.baseRectangleSection = baseRectangleSection;
        }
    }
}
