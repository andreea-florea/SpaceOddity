using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewInterface
{
    public interface ICurveRenderableFactory
    {
        IRenderable CreateRenderable(ICurve curve);
    }
}
