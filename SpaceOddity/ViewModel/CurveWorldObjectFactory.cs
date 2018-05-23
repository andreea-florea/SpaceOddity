using Algorithms;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel
{
    public class CurveWorldObjectFactory : IFactory<IWorldObject, ICurve>
    {
        private IFactory<IRenderable, ICurve> renderableFactory;

        public CurveWorldObjectFactory(IFactory<IRenderable, ICurve> renderableFactory)
        {
            this.renderableFactory = renderableFactory;
        }

        public IWorldObject Create(ICurve details)
        {
            var renderable = renderableFactory.Create(details);
            return new WorldObject(new Vector2(), new Vector2(), new Vector2(1, 1),
                new NoAction(), new NoAction(), renderable);
        }
    }
}
