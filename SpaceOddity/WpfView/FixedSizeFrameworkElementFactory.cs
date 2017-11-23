using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfView
{
    public class FixedSizeFrameworkElementFactory : IFrameworkElementFactory
    {
        private IFrameworkElementFactory basicFactory;
        private Vector2 baseElementScale;

        public FixedSizeFrameworkElementFactory(IFrameworkElementFactory basicFactory, Vector2 baseElementScale)
        {
            this.basicFactory = basicFactory;
            this.baseElementScale = baseElementScale;
        }

        public FrameworkElement CreateElement()
        {
            var parent = new Canvas();
            
            var baseElement = basicFactory.CreateElement();
            baseElement.Height = baseElementScale.Y;
            baseElement.Width = baseElementScale.X;
            Canvas.SetLeft(baseElement, -baseElementScale.X * 0.5);
            Canvas.SetTop(baseElement, -baseElementScale.Y * 0.5);

            parent.Children.Add(baseElement);
            return parent;
        }
    }
}
