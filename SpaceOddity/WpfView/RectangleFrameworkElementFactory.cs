using Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfView
{
    public class RectangleFrameworkElementFactory : IFactory<IFrameworkElementWrapper>
    {
        private int zIndex;

        public RectangleFrameworkElementFactory(int zIndex)
        {
            this.zIndex = zIndex;
        }

        public IFrameworkElementWrapper Create()
        {
            var rectangle = new Rectangle();
            rectangle.StrokeThickness = 1;
            Canvas.SetZIndex(rectangle, zIndex);
            return new RectangleFrameworkElementWrapper(rectangle);
        }
    }
}
