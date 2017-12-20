using Algorithm;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfView
{
    public class GridParentFrameworkElementFactory : IFactory<IFrameworkElementWrapper>
    {
        private IFactory<IFrameworkElementWrapper> baseElementFactory;
        private Vector2 scale;

        public GridParentFrameworkElementFactory(IFactory<IFrameworkElementWrapper> baseElementFactory, Vector2 scale)
        {
            this.baseElementFactory = baseElementFactory;
            this.scale = scale;
        }

        public IFrameworkElementWrapper Create()
        {
            var grid = new Grid();

            var baseElement = baseElementFactory.Create();
            baseElement.Element.Width = scale.X;
            baseElement.Element.Height = scale.Y;
            grid.Children.Add(baseElement.Element);

            return new ParentFrameworkElementWrapper(baseElement, grid);
        }
    }
}
