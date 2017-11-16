using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ViewInterface;

namespace WpfView
{
    public class WpfRenderableFactory : IRenderableFactory
    {
        private Canvas parentCanvas;
        private IFrameworkElementFactory elementFactory;

        public WpfRenderableFactory(Canvas parentCanvas, IFrameworkElementFactory elementFactory)
        {
            this.parentCanvas = parentCanvas;
            this.elementFactory = elementFactory;
        }

        public IRenderable CreateRenderable()
        {
            var element = elementFactory.CreateElement();
            parentCanvas.Children.Add(element);
            return new WpfRenderable(element, parentCanvas);
        }
    }
}
