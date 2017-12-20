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

        public FixedSizeFrameworkElementFactory(IFactory<IFrameworkElementWrapper> basicFactory, Vector2 baseElementScale)
        {
            this.basicFactory = basicFactory;
            this.baseElementScale = baseElementScale;
        }

        public IFrameworkElementWrapper Create()
        {
            var parent = new Canvas();
            
            var baseElement = basicFactory.Create();
            baseElement.Element.Height = baseElementScale.Y;
            baseElement.Element.Width = baseElementScale.X;
            Canvas.SetLeft(baseElement.Element, -baseElementScale.X * 0.5);
            Canvas.SetTop(baseElement.Element, -baseElementScale.Y * 0.5);

            parent.Children.Add(baseElement.Element);
            return new ParentFrameworkElementWrapper(baseElement, parent);
        }
    }
}
