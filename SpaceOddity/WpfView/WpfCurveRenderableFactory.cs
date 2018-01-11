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
        private int zIndex;
        private int lineSegments;

        public WpfCurveRenderableFactory(Canvas parentCanvas, 
            BuilderWorldObjectState[] states,
            int zIndex,
            int lineSegments)
        {
            this.parentCanvas = parentCanvas;
            this.states = states;
            this.zIndex = zIndex;
            this.lineSegments = lineSegments;
        }

        public IRenderable CreateRenderable(ICurve curve)
        {
            var lines = CreateLines();
            var renderable = new WpfCurveRenderable(lines, curve, parentCanvas, states);
            renderable.SetState(0);
            return renderable;
        }

        private IList<Line> CreateLines()
        {
            var lines = new List<Line>();
            for (var i = 0; i < lineSegments; ++i)
            {
                var line = new Line();
                line.StrokeThickness = 2;
                parentCanvas.Children.Add(line);
                Canvas.SetZIndex(line, zIndex);
                lines.Add(line);
            }
            return lines;
        }
    }
}
