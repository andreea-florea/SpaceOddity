using Algorithm;
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
        private IFactory<IFrameworkElementWrapper> elementFactory;
        private BuilderWorldObjectState[] states;

        public WpfRenderableFactory(Canvas parentCanvas, IFactory<IFrameworkElementWrapper> elementFactory, BuilderWorldObjectState[] states)
        {
            this.parentCanvas = parentCanvas;
            this.elementFactory = elementFactory;
            this.states = states;
        }

        public IRenderable CreateRenderable()
        {
            var wrapper = elementFactory.Create();
            parentCanvas.Children.Add(wrapper.Element);
            var renderable = new WpfRenderable(wrapper, parentCanvas, states);
            renderable.SetState(0);
            return renderable;
        }
    }
}
