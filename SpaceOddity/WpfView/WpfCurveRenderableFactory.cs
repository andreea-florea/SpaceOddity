using Algorithm;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using ViewInterface;

namespace WpfView
{
    public class WpfCurveRenderableFactory : ICurveRenderableFactory
    {
        private Canvas parentCanvas;
        private BuilderWorldObjectState[] states;

        public WpfCurveRenderableFactory(Canvas parentCanvas, 
            BuilderWorldObjectState[] states)
        {
            this.parentCanvas = parentCanvas;
            this.states = states;
        }

        public IRenderable CreateRenderable(ICurve curve)
        {
            var line = new Line();
            line.StrokeThickness = 2;
            parentCanvas.Children.Add(line);
            var renderable = new WpfCurveRenderable(line, curve, parentCanvas, states);
            renderable.SetState(0);
            return renderable;
        }
    }
}
