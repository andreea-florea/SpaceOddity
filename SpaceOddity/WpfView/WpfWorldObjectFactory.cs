using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ViewInterface;

namespace WpfView
{
    public class WpfWorldObjectFactory : IWorldObjectFactory
    {
        private Canvas canvas;
        private IFrameworkElementFactory frameworkElementFactory;

        public WpfWorldObjectFactory(Canvas canvas, IFrameworkElementFactory frameworkElementFactory)
        {
            this.canvas = canvas;
            this.frameworkElementFactory = frameworkElementFactory;
        }

        public IWorldObject CreateObject()
        {
            var frameworkElement = frameworkElementFactory.CreateElement();
            canvas.Children.Add(frameworkElement);
            return new WpfWorldObject(frameworkElement, new Vector2(), new Vector2(), 
                new NoAction(), new NoAction());
        }
    }
}
