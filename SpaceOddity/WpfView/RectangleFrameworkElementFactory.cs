using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfView
{
    public class RectangleFrameworkElementFactory : IFrameworkElementFactory
    {
        public IFrameworkElementWrapper CreateElement()
        {
            var rectangle = new Rectangle();
            rectangle.StrokeThickness = 1;
            return new RectangleFrameworkElementWrapper(rectangle);
        }
    }
}
