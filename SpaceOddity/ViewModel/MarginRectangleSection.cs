using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ViewModel.Interfaces;

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
                return new Rectangle(baseSection.TopLeftCorner + margin, baseSection.BottomRightCorner - margin);
            }
        }

        public MarginRectangleSection(Vector2 margin, IRectangleSection baseRectangleSection)
        {
            this.margin = margin;
            this.baseRectangleSection = baseRectangleSection;
        }
    }
}
