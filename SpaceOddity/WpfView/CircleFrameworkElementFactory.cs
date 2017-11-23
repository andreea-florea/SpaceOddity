using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfView
{
    public class CircleFrameworkElementFactory : IFrameworkElementFactory
    {
        public FrameworkElement CreateElement()
        {
            var circle = new Ellipse();
            circle.Stroke = Brushes.Orange;
            circle.Fill = Brushes.Yellow;
            circle.StrokeThickness = 1;
            return circle;
        }
    }
}
