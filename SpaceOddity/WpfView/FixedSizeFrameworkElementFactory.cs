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
    public class FixedSizeFrameworkElementFactory : IFactory<IFrameworkElementWrapper>
    {
        private IFactory<IFrameworkElementWrapper> basicFactory;
        private Vector2 baseElementScale;
        private int zIndex;

        public FixedSizeFrameworkElementFactory(IFactory<IFrameworkElementWrapper> basicFactory, Vector2 baseElementScale, int zIndex)
        {
            this.basicFactory = basicFactory;
            this.baseElementScale = baseElementScale;
            this.zIndex = zIndex;
        }

        public IFrameworkElementWrapper Create()
        {
            var parent = new Canvas();
            
            var baseElement = basicFactory.Create();
            baseElement.Element.Height = baseElementScale.Y;
            baseElement.Element.Width = baseElementScale.X;
            Canvas.SetLeft(baseElement.Element, -baseElementScale.X * 0.5);
            Canvas.SetTop(baseElement.Element, -baseElementScale.Y * 0.5);
            Canvas.SetZIndex(parent, zIndex);

            parent.Children.Add(baseElement.Element);
            return new ParentFrameworkElementWrapper(baseElement, parent);
        }
    }
}
