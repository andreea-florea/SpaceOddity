using Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfView
{
    public class CircleFrameworkElementFactory : IFactory<IFrameworkElementWrapper>
    {
        public IFrameworkElementWrapper Create()
        {
            var circle = new Ellipse();
            circle.StrokeThickness = 1;
            return new CircleFrameworkElementWrapper(circle);
        }
    }
}
