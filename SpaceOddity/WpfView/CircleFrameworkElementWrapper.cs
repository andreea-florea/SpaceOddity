using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfView
{
    public class CircleFrameworkElementWrapper : IFrameworkElementWrapper
    {
        private Ellipse circle;
        public FrameworkElement Element
        {
            get
            {
                return circle;
            }
        }

        public ColorVector Fill
        {
            set 
            {
                circle.Fill = new SolidColorBrush(value.GetColor());
            }
        }

        public ColorVector Border
        {
            set 
            {
                circle.Stroke = new SolidColorBrush(value.GetColor());
            }
        }

        public CircleFrameworkElementWrapper(Ellipse circle)
        {
            this.circle = circle;
        }
    }
}
