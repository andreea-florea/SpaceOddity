using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfView
{
    public class RectangleFrameworkElementFactory : IFrameworkElementFactory
    {
        private Brush fill;
        private Brush stroke;

        public RectangleFrameworkElementFactory(Brush fill, Brush stroke)
        {
            this.fill = fill;
            this.stroke = stroke;
        }

        public FrameworkElement CreateElement()
        {
            var rectangle = new Rectangle();
            rectangle.Stroke = stroke;
            rectangle.Fill = fill;
            rectangle.StrokeThickness = 1;
            return rectangle;
        }
    }
}
