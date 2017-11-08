using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public class AspectRatioRectangleSection : IRectangleSection
    {
        private Vector2 aspectRatio;
        private IRectangleSection baseRectangleSection;

        public Rectangle Section
        {
            get 
            {
                var baseSection = baseRectangleSection.Section;
                
                var xScaled = baseSection.Dimensions.X / aspectRatio.X;
                var yScaled = baseSection.Dimensions.Y / aspectRatio.Y;
                var scalledAspectRatio = aspectRatio * Math.Min(xScaled, yScaled);

                var topCornerOffset = (baseSection.Dimensions - scalledAspectRatio) * 0.5;
                var sectionTopLeftCorner = baseSection.TopLeftCorner + topCornerOffset;

                return new Rectangle(sectionTopLeftCorner, sectionTopLeftCorner + scalledAspectRatio);
            }
        }

        public AspectRatioRectangleSection(Vector2 aspectRatio, IRectangleSection baseRectangleSection)
        {
            this.aspectRatio = aspectRatio;
            this.baseRectangleSection = baseRectangleSection;
        }
    }
}
