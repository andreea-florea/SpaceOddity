﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfView
{
    public class RectangleFrameworkElementWrapper : IFrameworkElementWrapper
    {
        private Rectangle rectangle;
        public FrameworkElement Element
        {
            get
            {
                return rectangle;
            }
        }

        public ColorVector Fill
        {
            set 
            {
                rectangle.Fill = new SolidColorBrush(value.GetColor());
            }
        }

        public ColorVector Border
        {
            set 
            {
                rectangle.Stroke = new SolidColorBrush(value.GetColor());
            }
        }

        public RectangleFrameworkElementWrapper(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }
    }
}
