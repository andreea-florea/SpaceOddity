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
    public class CircleFrameworkElementFactory : IFactory<IFrameworkElementWrapper>
    {
        private int zIndex;

        public CircleFrameworkElementFactory(int zIndex)
        {
            this.zIndex = zIndex;
        }

        public IFrameworkElementWrapper Create()
        {
            var circle = new Ellipse();
            circle.StrokeThickness = 1;
            Canvas.SetZIndex(circle, zIndex);
            return new CircleFrameworkElementWrapper(circle);
        }
    }
}
