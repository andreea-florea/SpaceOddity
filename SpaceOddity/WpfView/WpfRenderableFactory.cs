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
        private BuilderWorldObjectState[] states;

        public WpfRenderableFactory(Canvas parentCanvas, IFrameworkElementFactory elementFactory, BuilderWorldObjectState[] states)
        {
            this.parentCanvas = parentCanvas;
            this.elementFactory = elementFactory;
            this.states = states;
        }

        public IRenderable CreateRenderable()
        {
            var wrapper = elementFactory.CreateElement();
            parentCanvas.Children.Add(wrapper.Element);
            var renderable = new WpfRenderable(wrapper, parentCanvas, states);
            renderable.SetState(0);
            return renderable;
        }
    }
}
