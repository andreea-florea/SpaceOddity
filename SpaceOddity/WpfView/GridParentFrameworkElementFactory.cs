using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfView
{
    public class GridParentFrameworkElementFactory : IFrameworkElementFactory
    {
        private IFrameworkElementFactory baseElementFactory;
        private Vector2 scale;

        public GridParentFrameworkElementFactory(IFrameworkElementFactory baseElementFactory, Vector2 scale)
        {
            this.baseElementFactory = baseElementFactory;
            this.scale = scale;
        }

        public FrameworkElement CreateElement()
        {
            var grid = new Grid();

            var baseElement = baseElementFactory.CreateElement();
            baseElement.Width = scale.X;
            baseElement.Height = scale.Y;
            grid.Children.Add(baseElement);

            return grid;
        }
    }
}
